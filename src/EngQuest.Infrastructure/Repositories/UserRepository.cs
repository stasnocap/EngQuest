using System.Data;
using Dapper;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Application.Levels.GetLevel;
using EngQuest.Application.Users.LogInUser;
using EngQuest.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EngQuest.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<LogInResponse> GetLoginResponseAsync(LogInUserCommand command, IDbConnection dbConnection)
    {
        const string sql = """
                           SELECT 
                                u.id AS UserId, 
                                u.first_name AS FirstName, 
                                u.last_name AS LastName, 
                                u.email AS Email, 
                                u.identity_id AS IdentityId,
                                r.name AS Role
                           FROM users AS u
                           JOIN role_user AS ru ON ru.users_id  = u.id
                           JOIN roles AS r ON ru.roles_id = r.id
                           WHERE u.email = @Email
                           """;

        var roles = new List<string>();

        IEnumerable<LogInResponse> users = await dbConnection.QueryAsync<LogInResponse, string, LogInResponse>(sql, (user, role) =>
        {
            roles.Add(role);
            return user;
        }, command, splitOn: "Role");

        LogInResponse logIn = users.Single();

        logIn.Roles.AddRange(roles);

        return logIn;
    }


    public override void Add(User user)
    {
        foreach (Role role in user.Roles)
        {
            DbContext.Attach(role);
        }

        DbContext.Add(user);
    }

    public Task<bool> ExistsByIdentityIdAsync(string identityId, CancellationToken cancellationToken)
    {
        return DbContext.Set<User>().AnyAsync(x => x.IdentityId == identityId, cancellationToken);
    }

    public Task<User?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken)
    {
        return DbContext.Set<User>().FirstOrDefaultAsync(x => x.IdentityId == identityId, cancellationToken);
    }
}
