using System.Data;
using EngQuest.Application.Users.GetUsers;
using EngQuest.Application.Users.LogInUser;
using EngQuest.Domain.Users;

namespace EngQuest.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<LogInResponse> GetLoginResponseAsync(LogInUserCommand command, IDbConnection dbConnection);

    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<User?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken);
    
    Task<bool> ExistsByIdentityIdAsync(string identityId, CancellationToken cancellationToken);

    void Add(User user);

    Task<List<UserResponse>> GetAllAsync(IDbConnection dbConnection, CancellationToken cancellationToken);
}
