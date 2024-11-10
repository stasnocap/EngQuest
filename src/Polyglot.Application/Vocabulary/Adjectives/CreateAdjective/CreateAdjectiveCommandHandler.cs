﻿using Polyglot.Application.Abstractions.Messaging;
using Polyglot.Domain.Abstractions;
using Polyglot.Domain.Shared;
using Polyglot.Domain.Vocabulary;
using Polyglot.Domain.Vocabulary.Adjectives;

namespace Polyglot.Application.Vocabulary.Adjectives.CreateAdjective;

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