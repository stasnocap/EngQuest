using EngQuest.Domain.Quests;

namespace EngQuest.Application.Abstractions.Repositories;

public interface IQuestRepository
{
    Task<List<Quest>> GetRangeAsync(int? userId, CancellationToken cancellationToken);
}
