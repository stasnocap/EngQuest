namespace EngQuest.Application.Objectives.GetRandomObjective;

public sealed class ObjectiveResponse
{
    public required int ObjectiveId { get; init; }
    
    public required int QuestId { get; init; }
    
    public required string RusPhrase { get; init; }
    
    public required string[][] WordGroups { get; init; }
}
