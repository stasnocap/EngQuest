#pragma warning disable S125

using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EngQuest.Application.Users.LogInUser;
using EngQuest.Application.Users.RegisterUser;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Users;
using EngQuest.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EngQuest.Infrastructure.Authentication;

public class OpenIdConnectOptionsSetup(IOptions<AuthenticationOptions> _authenticationOptions, IOptions<KeycloakOptions> _keyCloakOptions, IWebHostEnvironment _webHostEnvironment) : IConfigureNamedOptions<OpenIdConnectOptions>
{
    public void Configure(OpenIdConnectOptions options)
    {
        bool isDevelopment = _webHostEnvironment.IsDevelopment();

        options.RequireHttpsMetadata = _authenticationOptions.Value.RequireHttpsMetadata;
        options.Authority = _authenticationOptions.Value.Issuer;
        // options.Authority = "http://localhost:18080/realms/engquest";
        options.ClientId = _keyCloakOptions.Value.AuthClientId;
        // options.ClientSecret = "RJimxkXfscfvi76sgDqiTpMQ0D63kL8b";
        options.ClientSecret = _keyCloakOptions.Value.AuthClientSecret;
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.Scope.Add("openid");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "preferred_username",
            RoleClaimType = "roles",
        };

        // for (localhost   docker) authorization code flow to work
        if (isDevelopment)
        {
            options.TokenValidationParameters.ValidIssuer = "http://localhost:18080/realms/engquest";
        }

        options.Events.OnTicketReceived = async ctx =>
        {
            ClaimsPrincipal principal = ctx.Principal!;

            ISender sender = ctx.HttpContext.RequestServices.GetRequiredService<ISender>();
            ILogger<OpenIdConnectOptionsSetup> logger = ctx.HttpContext.RequestServices.GetRequiredService<ILogger<OpenIdConnectOptionsSetup>>();

            var command = new RegisterUserCommand(
                principal.GetEmail()!,
                principal.GetFirstName()!,
                principal.GetLastName()!,
                1,
                0,
                IdentityId: principal.GetIdentityId()
            );

            Result<LogInResponse> result = await sender.Send(command);

            if (result.IsFailure)
            {
                logger.LogError("During registering user from IdentityProvider occured error: {Error}", result.Error.ToString());
            }

            CustomClaimsTransformation claimsTransformation = ctx.HttpContext.RequestServices.GetRequiredService<CustomClaimsTransformation>();

            await claimsTransformation.TransformAsync(principal);
        };

        options.Events.OnRedirectToIdentityProvider = ctx =>
        {
            if (!ctx.HttpContext.Request.Path.Value?.EndsWith("/login", StringComparison.InvariantCulture) ?? true)
            {
                ctx.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                ctx.HandleResponse();
            }
            else
            {
                // for (localhost   docker) authorization code flow to work
                if (isDevelopment)
                {
                    ctx.ProtocolMessage.IssuerAddress = "http://localhost:18080/realms/engquest/protocol/openid-connect/auth";
                }
            }

            return Task.CompletedTask;
        };

        options.Events.OnRedirectToIdentityProviderForSignOut = (ctx) =>
        {
            // for (localhost   docker) authorization code flow to work
            if (isDevelopment)
            {
                ctx.ProtocolMessage.IssuerAddress = "http://localhost:18080/realms/engquest/protocol/openid-connect/logout";
            }

            return Task.CompletedTask;
        };
    }

    public void Configure(string? name, OpenIdConnectOptions options)
    {
        Configure(options);
    }
}