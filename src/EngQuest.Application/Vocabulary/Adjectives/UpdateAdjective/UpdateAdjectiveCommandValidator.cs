﻿using FluentValidation;

namespace EngQuest.Application.Vocabulary.Adjectives.UpdateAdjective;

public class UpdateAdjectiveCommandValidator : AbstractValidator<UpdateAdjectiveCommand>
{
    public UpdateAdjectiveCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
        RuleFor(a => a.Text).NotEmpty();
    }
}