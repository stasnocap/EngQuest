using EngQuest.Application.Abstractions.Authentication;
using EngQuest.Application.Abstractions.Caching;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Application.Abstractions.Repositories.Objectives;
using EngQuest.Application.Levels.GetLevel;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Levels;
using EngQuest.Domain.Objectives;

namespace EngQuest.Application.Objectives.CompleteObjective;

public class CompleteObjectiveCommandHandler(
    IObjectiveRepository _objectiveRepository,
    ILevelRepository _levelRepository,
    IUnitOfWork _unitOfWork,
    IUserContext _userContext,
    ICacheService _cacheService) : ICommandHandler<CompleteObjectiveCommand, CompleteObjectiveResponse>
{
    public async Task<Result<CompleteObjectiveResponse>> Handle(CompleteObjectiveCommand request, CancellationToken cancellationToken)
    {
        Objective? objective = await _objectiveRepository.GetByIdAsync(request.ObjectiveId, request.QuestId, cancellationToken);

        if (objective is null)
        {
            return Result.Failure<CompleteObjectiveResponse>(ObjectiveErrors.NotFound);
        }

        int? userId = _userContext.UserId;

        CompleteObjectiveResult result = objective.Complete(request.Answer, request.QuestId, userId);

        var response = new CompleteObjectiveResponse
        {
            CompleteObjectiveResult = result
        };

        if (userId is null)
        {
            return response;
        }

        Level? level = await _levelRepository.GetByUserIdAsync(userId.Value, cancellationToken);

        if (level is null)
        {
            return response;
        }

        GainExperienceResult gainExperienceResult = level.GainExperience(request.QuestId);

        await _cacheService.RemoveAsync($"level-{userId}", cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        response.Level = new LevelResponse
        {
            Value = level.Value,
            Experience = level.Experience,
        };
        
        response.GainExperienceResult = gainExperienceResult;

        return response;
    }
}
