using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Asp.Versioning;
using Dapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using EngQuest.Application.Abstractions.Authentication;
using EngQuest.Application.Abstractions.Caching;
using EngQuest.Application.Abstractions.Clock;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Application.Abstractions.Email;
using EngQuest.Infrastructure.Authentication;
using EngQuest.Infrastructure.Authorization;
using EngQuest.Infrastructure.Caching;
using EngQuest.Infrastructure.Clock;
using EngQuest.Infrastructure.Data;
using EngQuest.Infrastructure.Email;
using EngQuest.Infrastructure.Outbox;
using EngQuest.Infrastructure.Repositories;
using EngQuest.Infrastructure.Repositories.Vocabulary;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Levels;
using EngQuest.Domain.Objectives;
using EngQuest.Domain.Quests;
using EngQuest.Domain.Users;
using EngQuest.Domain.Vocabulary;
using EngQuest.Domain.Vocabulary.Adjectives;
using Quartz;
using AuthenticationOptions = EngQuest.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = EngQuest.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = EngQuest.Application.Abstractions.Authentication.IAuthenticationService;

namespace EngQuest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddTransient<IEmailService, EmailService>();

        AddPersistence(services, configuration);

        AddCaching(services, configuration);

        AddAuthentication(services, configuration);

        AddAuthorization(services);

        AddHealthChecks(services, configuration);

        AddApiVersioning(services);

        AddBackgroundJobs(services, configuration);

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILevelRepository, LevelRepository>();
        services.AddScoped<IQuestRepository, QuestRepository>();
        services.AddScoped<IObjectiveRepository, ObjectiveRepository>();

        services.AddScoped<IVocabularyRepository, VocabularyRepository>();
        services.AddScoped<IAdjectiveRepository, AdjectiveRepository>();
        services.AddScoped<ComparisonAdjectiveRepository>();
        services.AddScoped<NounRepository>();
        services.AddScoped<VerbRepository>();
        services.AddScoped<PrimaryVerbRepository>();
        services.AddScoped<LetterNumberRepository>();
        services.AddScoped<ModalVerbRepository>();
        services.AddScoped<NumberWithNounRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        AddSnakeCaseMapping();
    }

    private static void AddSnakeCaseMapping()
    {
        foreach (Type type in new Assembly[] { typeof(DependencyInjection).Assembly, typeof(Application.DependencyInjection).Assembly }
                    .SelectMany(x => x.GetTypes())
                    .Where(t => t.GetCustomAttribute<SnakeCaseMappingAttribute>() != null))
        {
            SqlMapper.SetTypeMap(type, new CustomPropertyTypeMap(type, (t, columnName) =>
            {
                string propertyName = columnName.Replace("_", "", StringComparison.OrdinalIgnoreCase);
                return t.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase) ?? throw new InvalidOperationException($"Property '{propertyName}' not found on type '{t}'");
            }));
        }
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        services.ConfigureOptions<CookieAuthenticationOptionsSetup>();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

        services.AddTransient<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
        {
            KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddScoped<AuthorizationService>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }

    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Cache") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Cache")!)
            .AddUrlGroup(new Uri(configuration["KeyCloak:BaseUrl"]!), HttpMethod.Get, "keycloak");
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    [SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed")]
    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }
}
