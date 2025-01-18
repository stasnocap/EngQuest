using EngQuest.Domain.Users;

namespace EngQuest.Infrastructure.Authorization;

public sealed class UserRolesResponse
{
    public int UserId { get; init; }

    public List<Role> Roles { get; init; } = [];
}
