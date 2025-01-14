using EngQuest.Application.Abstractions.Data;
using EngQuest.Domain.Objectives;

namespace EngQuest.Application.Abstractions.Repositories.Objectives;

[SnakeCaseMapping]
public class WordDto
{
    public required int Number { get; init; }
    public required string Text { get; init; }
    public required WordType Type { get; init; }
}
