using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using EngQuest.Domain.Quests;
using EngQuest.Application.Abstractions.Repositories;

namespace EngQuest.Infrastructure.Repositories;

[SuppressMessage("Info Code Smell", "S1135:Track uses of \"TODO\" tags")]
internal sealed class QuestRepository(ApplicationDbContext dbContext) : Repository<Quest>(dbContext), IQuestRepository
{
    public Task<bool> ExistsAsync(int questId, CancellationToken cancellationToken)
    {
        return DbContext
            .Set<Quest>()
            .AnyAsync(l => l.Id == questId, cancellationToken);
    }

    // TODO: fix this
    public Task<List<Quest>> GetRangeAsync(int? userId, CancellationToken cancellationToken)
    {
        return DbContext
            .Set<Quest>()
            .OrderBy(l => l.Id)
            .Take(15)
            .ToListAsync(cancellationToken);
    }
}
