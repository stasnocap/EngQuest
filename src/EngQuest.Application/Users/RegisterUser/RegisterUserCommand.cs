using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Users.LogInUser;

namespace EngQuest.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    int Level,
    int Experience,
    string? Password = null,
    string? IdentityId = null) : ICommand<LogInResponse>;
