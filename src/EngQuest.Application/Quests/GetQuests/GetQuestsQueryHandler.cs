using EngQuest.Application.Abstractions.Authentication;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Quests;

namespace EngQuest.Application.Quests.GetQuests;

public class GetQuestsQueryHandler(IQuestRepository _questRepository, IUserContext _userContext) : IQueryHandler<GetQuestsQuery, IReadOnlyList<QuestResponse>>
{
    public async Task<Result<IReadOnlyList<QuestResponse>>> Handle(GetQuestsQuery request, CancellationToken cancellationToken)
    {
        int? currentUserId = _userContext.UserId;

        List<Quest> quests = await _questRepository.GetRangeAsync(currentUserId, cancellationToken);

        return Result.Success<IReadOnlyList<QuestResponse>>(quests.Select(x => new QuestResponse
        {
            Name = x.Name.Value,
            Id = x.Id
        }).ToList());
    }
}
