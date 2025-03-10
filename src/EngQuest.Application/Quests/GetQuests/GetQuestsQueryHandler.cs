using System.Data;
using EngQuest.Application.Abstractions.Authentication;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Quests;

namespace EngQuest.Application.Quests.GetQuests;

public class GetQuestsQueryHandler(ISqlConnectionFactory _sqlConnectionFactory, IQuestRepository _questRepository) : IQueryHandler<GetQuestsQuery, IReadOnlyList<QuestResponse>>
{
    public async Task<Result<IReadOnlyList<QuestResponse>>> Handle(GetQuestsQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection dbConnection = _sqlConnectionFactory.CreateConnection();

        List<QuestResponse> quests = await _questRepository.GetAllAsync(dbConnection);

        return quests;
    }
}
