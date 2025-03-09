using System.Data;
using EngQuest.Application.Quests.GetQuests;
using EngQuest.Domain.Quests;

namespace EngQuest.Application.Abstractions.Repositories;

public interface IQuestRepository
{
    Task<List<QuestResponse>> GetAllAsync(IDbConnection dbConnection);
}
