using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.Adjectives;

namespace EngQuest.Application.Vocabulary.Adjectives.UpdateAdjective;

public class UpdateAdjectiveCommandHandler(IAdjectiveRepository _repository, IUnitOfWork _unitOfWork) : ICommandHandler<UpdateAdjectiveCommand>
{
    public async Task<Result> Handle(UpdateAdjectiveCommand request, CancellationToken cancellationToken)
    {
        Adjective? adjective = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (adjective is null)
        {
            return Result.Failure(AdjectiveErrors.NotFound);
        }

        var text = new Text(request.Text);
        
        if (await _repository.ExistsAsync(text, cancellationToken))
        {
            return Result.Failure(AdjectiveErrors.AlreadyExists);
        }
        
        adjective.SetText(text);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
