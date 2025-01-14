using System.Data;
using EngQuest.Application.Users.LogInUser;
using EngQuest.Domain.Users;

namespace EngQuest.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<LogInResponse> GetLoginResponseAsync(LogInUserCommand command, IDbConnection dbConnection);

    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    void Add(User user);
}
