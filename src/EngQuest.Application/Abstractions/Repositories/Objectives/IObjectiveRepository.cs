using System.Data;
using EngQuest.Domain.Objectives;

namespace EngQuest.Application.Abstractions.Repositories.Objectives;

public partial interface IObjectiveRepository
{
    Task<ObjectiveDto> GetRandomAsync(int questId, IDbConnection dbConnection);
    Task<Objective?> GetByIdAsync(int objectiveId, int questId, CancellationToken cancellationToken);
}
