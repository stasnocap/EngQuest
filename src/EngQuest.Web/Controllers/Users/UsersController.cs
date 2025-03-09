#pragma warning disable S125 // Sections of code should not be commented out
using System.Security.Claims;
using Asp.Versioning;
using EngQuest.Application.Levels.GetLevel;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngQuest.Application.Users.LogInUser;
using EngQuest.Application.Users.RegisterUser;
using EngQuest.Domain.Abstractions;
using EngQuest.Infrastructure.Authentication;
using EngQuest.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Globalization;
using EngQuest.Domain.Users;

namespace EngQuest.Web.Controllers.Users;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/users")]
public class UsersController(ISender _sender) : ControllerBase
{
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLoggedInUser()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return NoContent();
        }

        int userId = User.GetUserId()!.Value;

        Result<LevelResponse> result = await _sender.Send(new GetLevelQuery(userId));

        if (result.IsFailure)
        {
            throw new Exception(result.Error.ToString());
        }

        LevelResponse level = result.Value;

        var userResponse = new UserResponse
        {
            FirstName = User.GetFirstName()!,
            LastName = User.GetLastName()!,
            Email = User.GetEmail()!,
            Level = level,
        };

        return Ok(userResponse);
    }

    [Authorize]
    [HttpGet("login")]
    public IActionResult LogIn(Uri? redirectUri)
    {
        return Redirect(redirectUri?.ToString() ?? "/");
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task LogOut(Uri? redirectUri)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = redirectUri?.ToString() });
    }
}
