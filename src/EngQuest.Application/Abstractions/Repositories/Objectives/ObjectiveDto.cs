using EngQuest.Application.Abstractions.Data;
using EngQuest.Domain.Objectives;

namespace EngQuest.Application.Abstractions.Repositories.Objectives;

[SnakeCaseMapping]
public class ObjectiveDto
{
    public required int Id { get; init; }
    public required string RusPhrase { get; init; }
    public HashSet<WordDto> Words { get; init; } = [];
 }
