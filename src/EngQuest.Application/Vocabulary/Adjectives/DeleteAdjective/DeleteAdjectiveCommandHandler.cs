using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Vocabulary;
using EngQuest.Domain.Vocabulary.Adjectives;

namespace EngQuest.Application.Vocabulary.Adjectives.DeleteAdjective;

public class DeleteAdjectiveCommandHandler(IAdjectiveRepository _repository, IUnitOfWork _unitOfWork) : ICommandHandler<DeleteAdjectiveCommand>
{
    public async Task<Result> Handle(DeleteAdjectiveCommand request, CancellationToken cancellationToken)
    {
        Adjective? adjective = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (adjective is null)
        {
            return Result.Failure(AdjectiveErrors.NotFound);
        }

        _repository.Delete(adjective);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
