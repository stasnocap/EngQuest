using Microsoft.EntityFrameworkCore;
using EngQuest.Application.Abstractions.Caching;
using EngQuest.Domain.Users;
using System.Collections;

namespace EngQuest.Infrastructure.Authorization;

public sealed class AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
{
    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        string cacheKey = $"auth:roles-{identityId}";
        UserRolesResponse? cachedRoles = await cacheService.GetAsync<UserRolesResponse>(cacheKey);

        if (cachedRoles is not null)
        {
            return cachedRoles;
        }

        UserRolesResponse roles = await dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                Roles = u.Roles.Select(x => new Role(x.Id, x.Name)
                {
                    Users = Array.Empty<User>(),
                }).ToList()
            })
            .FirstAsync();

        await cacheService.SetAsync(cacheKey, roles);

        return roles;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        string cacheKey = $"auth:permissions-{identityId}";
        HashSet<string>? cachedPermissions = await cacheService.GetAsync<HashSet<string>>(cacheKey);

        if (cachedPermissions is not null)
        {
            return cachedPermissions;
        }

        ICollection<Permission> permissions = await dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .SelectMany(u => u.Roles.Select(r => r.Permissions))
            .FirstAsync();

        var permissionsSet = permissions.Select(p => p.Name).ToHashSet();

        await cacheService.SetAsync(cacheKey, permissionsSet);

        return permissionsSet;
    }
}
