using EngQuest.Domain.Objectives;
using FluentAssertions;
using EngQuest.Domain.Shared;

namespace EngQuest.Domain.UnitTests.Quests.Objectives;

public class WordDecoratorServiceTests
{
    [Fact]
    public void Decorate_Should_AppendNonWordSymbol_IfWordHasNonWordSymbolAtTheEnd()
    {
        string word = "my.";
        var words = new List<string>()
        {
            "old",
            "new",
            "granny",
        };
        
        WordDecoratorService.Decorate(word, words);
        
        foreach (string w in words)
        {
            (w[^1] == '.').Should().BeTrue();
        }
    }
    
    [Fact]
    public void Decorate_Should_DoNothing_IfWordHasNonWordSymbolInTheMiddle()
    {
        string word = "didn't";
        var source = new List<string>()
        {
            "old",
            "new",
            "granny",
        };
        
        var decorated = new List<string>()
        {
            "old",
            "new",
            "granny",
        };
        
        WordDecoratorService.Decorate(word, decorated);

        source.SequenceEqual(decorated).Should().BeTrue();
    }
}
