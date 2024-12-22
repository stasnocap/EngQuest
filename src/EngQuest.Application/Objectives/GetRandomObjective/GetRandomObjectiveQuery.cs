using EngQuest.Application.Abstractions.Authorization;

namespace EngQuest.Application.Objectives.GetRandomObjective;

public record GetRandomObjectiveQuery(int QuestId) : IAuthorizedForQuestRequest<ObjectiveResponse>;
