﻿using Polyglot.Application.Abstractions.Messaging;

namespace Polyglot.Application.Vocabulary.Adjectives.DeleteAdjective;

public record DeleteAdjectiveCommand(int Id) : ICommand;