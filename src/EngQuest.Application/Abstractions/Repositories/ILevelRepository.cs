using System.Data;
using EngQuest.Application.Levels.GetLevel;
using EngQuest.Domain.Levels;

namespace EngQuest.Application.Abstractions.Repositories;

public interface ILevelRepository
{
    Task<LevelResponse> GetLevelAsync(int userId, IDbConnection dbConnection);
    Task<Level?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
    void Add(Level level);
}
