using System.Data;
using Dapper;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Application.Levels.GetLevel;
using EngQuest.Domain.Levels;
using Microsoft.EntityFrameworkCore;

namespace EngQuest.Infrastructure.Repositories;

public class LevelRepository(ApplicationDbContext dbContext) : Repository<Level>(dbContext), ILevelRepository
{
    public async Task<LevelResponse> GetLevelAsync(int userId, IDbConnection dbConnection)
    {
        const string sql = """
                           SELECT level as Value, level_xp as Experience from levels
                           WHERE user_id = @UserId
                           LIMIT 1
                           """;

        LevelResponse? levelResponse = await dbConnection.QueryFirstOrDefaultAsync<LevelResponse>(sql, new { userId });

        return levelResponse;
    }

    public Task<Level?> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return DbContext
            .Set<Level>()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}
