using System.Data;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Levels;

namespace EngQuest.Application.Levels.GetLevel;

public class GetLevelQueryHandler(ISqlConnectionFactory _sqlConnectionFactory, ILevelRepository _levelRepository) : IQueryHandler<GetLevelQuery, LevelResponse>
{
    public async Task<Result<LevelResponse>> Handle(GetLevelQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection dbConnection = _sqlConnectionFactory.CreateConnection();

        LevelResponse? levelResponse = await _levelRepository.GetLevelAsync(request.UserId, dbConnection);

        if (levelResponse is null)
        {
            return Result.Failure<LevelResponse>(LevelErrors.NotFound);
        }

        return levelResponse;
    }
}
