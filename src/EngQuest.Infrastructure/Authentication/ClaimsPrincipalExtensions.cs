using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EngQuest.Domain.Users;

namespace EngQuest.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static int? GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return int.TryParse(userId, out int parsedUserId) ? parsedUserId : null;
    }

    public static string? GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static string? GetFirstName(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.GivenName);
    }

    public static string? GetLastName(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.Surname);
    }

    public static string? GetEmail(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.Email);
    }
}
