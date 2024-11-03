using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.RequireHttpsMetadata = false;
        options.Authority = "http://localhost:8080/realms/myrealm";
        options.ClientId = "myclient";
        options.ClientSecret = "hwk7GqcMe2cOEkqHAZji6RqI9DRMWe7I";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.Scope.Add("openid");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "preferred_username",
            RoleClaimType = "roles"
        };

        options.Events.OnRedirectToIdentityProvider = RedirectOnlyOnLoginEndpoint;

        Task RedirectOnlyOnLoginEndpoint(RedirectContext redirectContext)
        {
            if (redirectContext.HttpContext.Request.Path != "/login")
            {
                redirectContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                redirectContext.HttpContext.Response.Headers["Location"] = "/login";
                redirectContext.HandleResponse();
            }
            
            return Task.CompletedTask;
        }
    });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();