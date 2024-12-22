using EngQuest.Domain.Objectives;
using Microsoft.EntityFrameworkCore;

namespace EngQuest.Infrastructure.Repositories;

public class ObjectiveRepository(ApplicationDbContext dbContext) : Repository<Objective>(dbContext), IObjectiveRepository
{
    public Task<Objective?> GetByIdAsync(int objectiveId, int questId, CancellationToken cancellationToken)
    {
        return DbContext
            .Set<Objective>()
            .FirstOrDefaultAsync(x => x.Id == objectiveId && x.QuestIds.Any(y => y.Value == questId), cancellationToken);
    }
}
