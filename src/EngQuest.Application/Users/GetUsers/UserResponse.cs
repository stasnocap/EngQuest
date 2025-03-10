using EngQuest.Application.Abstractions.Data;
using EngQuest.Application.Levels.GetLevel;

namespace EngQuest.Application.Users.GetUsers;

[SnakeCaseMapping]
public class UserResponse
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required LevelResponse Level { get; set; }
}
