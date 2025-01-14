using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary;
using EngQuest.Domain.Vocabulary.Adjectives;

namespace EngQuest.Application.Vocabulary.Adjectives.CreateAdjective;

public class CreateAdjectiveCommandHandler(IAdjectiveRepository _adjectiveRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CreateAdjectiveCommand, int>
{
    public async Task<Result<int>> Handle(CreateAdjectiveCommand request, CancellationToken cancellationToken)
    {
        var text = new Text(request.Text);

        if (await _adjectiveRepository.ExistsAsync(text, cancellationToken))
        {
            return Result.Failure<int>(AdjectiveErrors.AlreadyExists);
        }
        
        var adjective = new Adjective(text);

        _adjectiveRepository.Add(adjective);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return adjective.Id;
    }
}
