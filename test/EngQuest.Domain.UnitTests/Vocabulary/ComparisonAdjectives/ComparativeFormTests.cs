using FluentAssertions;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.ComparisonAdjectives;

namespace EngQuest.Domain.UnitTests.Vocabulary.ComparisonAdjectives;

public class ComparativeFormTests
{
    [Fact]
    public void Is_Should_ReturnTrue_IfAdjectiveStarsWithMore()
    {
        // Arrange
        var comparativeForm = new Text("more dangerous");
        
        // Act
        bool result = ComparativeForm.Is(comparativeForm);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void Is_Should_ReturnTrue_IfAdjectiveEndsWithEr()
    {
        // Arrange
        var comparativeForm = new Text("lower");
        
        // Act
        bool result = ComparativeForm.Is(comparativeForm);

        // Assert
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("neat", "neater", 1)]
    [InlineData("moist", "moister", 1)]
    [InlineData("near", "nearer", 1)]
    [InlineData("mild", "milder", 1)]
    [InlineData("low", "lower", 1)]
    [InlineData("loud", "louder", 1)]
    [InlineData("mean", "meaner", 1)]
    [InlineData("mad", "madder", 1)]
    [InlineData("new", "newer", 1)]
    [InlineData("quick", "quicker", 1)]
    [InlineData("pure", "purer", 1)]
    [InlineData("rare", "rarer", 1)]
    [InlineData("quiet", "quieter", 1)]
    [InlineData("proud", "prouder", 1)]
    [InlineData("odd", "odder", 1)]
    [InlineData("nice", "nicer", 1)]
    [InlineData("poor", "poorer", 1)]
    [InlineData("plain", "plainer", 1)]
    [InlineData("great", "greater", 1)]
    [InlineData("grave", "graver", 1)]
    [InlineData("hard", "harder", 1)]
    [InlineData("gross", "grosser", 1)]
    [InlineData("grand", "grander", 1)]
    [InlineData("fresh", "fresher", 1)]
    [InlineData("flat", "flatter", 1)]
    [InlineData("gentle", "gentler", 1)]
    [InlineData("full", "fuller", 1)]
    [InlineData("late", "later", 1)]
    [InlineData("kind", "kinder", 1)]
    [InlineData("little", "littler", 1)]
    [InlineData("light", "lighter", 1)]
    [InlineData("humble", "humbler", 1)]
    [InlineData("high", "higher", 1)]
    [InlineData("harsh", "harsher", 1)]
    [InlineData("hot", "hotter", 1)]
    [InlineData("hip", "hipper", 1)]
    [InlineData("sweet", "sweeter", 1)]
    [InlineData("strong", "stronger", 1)]
    [InlineData("thick", "thicker", 1)]
    [InlineData("tan", "tanner", 1)]
    [InlineData("strict", "stricter", 1)]
    [InlineData("sour", "sourer", 1)]
    [InlineData("sore", "sorer", 1)]
    [InlineData("strange", "stranger", 1)]
    [InlineData("steep", "steeper", 1)]
    [InlineData("wide", "wider", 1)]
    [InlineData("wet", "wetter", 1)]
    [InlineData("young", "younger", 1)]
    [InlineData("wild", "wilder", 1)]
    [InlineData("weird", "weirder", 1)]
    [InlineData("true", "truer", 1)]
    [InlineData("tough", "tougher", 1)]
    [InlineData("weak", "weaker", 1)]
    [InlineData("warm", "warmer", 1)]
    [InlineData("soon", "sooner", 1)]
    [InlineData("safe", "safer", 1)]
    [InlineData("sad", "sadder", 1)]
    [InlineData("sharp", "sharper", 1)]
    [InlineData("sane", "saner", 1)]
    [InlineData("rude", "ruder", 1)]
    [InlineData("rich", "richer", 1)]
    [InlineData("raw", "rawer", 1)]
    [InlineData("rough", "rougher", 1)]
    [InlineData("ripe", "riper", 1)]
    [InlineData("smart", "smarter", 1)]
    [InlineData("small", "smaller", 1)]
    [InlineData("soft", "softer", 1)]
    [InlineData("smooth", "smoother", 1)]
    [InlineData("slow", "slower", 1)]
    [InlineData("shy", "shyer", 1)]
    [InlineData("short", "shorter", 1)]
    [InlineData("slim", "slimmer", 1)]
    [InlineData("simple", "simpler", 1)]
    [InlineData("fit", "fitter", 1)]
    [InlineData("calm", "calmer", 1)]
    [InlineData("dull", "duller", 1)]
    [InlineData("dry", "dryer", 1)]
    [InlineData("clear", "clearer", 1)]
    [InlineData("cheap", "cheaper", 1)]
    [InlineData("brief", "briefer", 1)]
    [InlineData("brave", "braver", 1)]
    [InlineData("dumb", "dumber", 1)]
    [InlineData("broad", "broader", 1)]
    [InlineData("bright", "brighter", 1)]
    [InlineData("blue", "bluer", 1)]
    [InlineData("damp", "damper", 1)]
    [InlineData("cute", "cuter", 1)]
    [InlineData("dark", "darker", 1)]
    [InlineData("dense", "denser", 1)]
    [InlineData("deep", "deeper", 1)]
    [InlineData("close", "closer", 1)]
    [InlineData("bold", "bolder", 1)]
    [InlineData("coarse", "coarser", 1)]
    [InlineData("cool", "cooler", 1)]
    [InlineData("cold", "colder", 1)]
    [InlineData("wise", "wiser", 1)]
    [InlineData("fast", "faster", 1)]
    [InlineData("few", "fewer", 1)]
    [InlineData("thin", "thinner", 1)]
    [InlineData("big", "bigger", 1)]
    [InlineData("clean", "cleaner", 1)]
    [InlineData("large", "larger", 1)]
    [InlineData("tall", "taller", 1)]
    [InlineData("fine", "finer", 1)]
    [InlineData("firm", "firmer", 1)]
    [InlineData("fierce", "fiercer", 1)]
    [InlineData("long", "longer", 1)]
    [InlineData("old", "older", 1)]
    [InlineData("fair", "fairer", 1)]
    [InlineData("faint", "fainter", 1)]
    [InlineData("fat", "fatter", 1)]
    [InlineData("black", "blacker", 1)]
    [InlineData("bland", "blander", 1)]
    [InlineData("sleepy", "sleepier", 2)]
    [InlineData("scary", "scarier", 2)]
    [InlineData("angry", "angrier", 2)]
    [InlineData("slimy", "slimier", 2)]
    [InlineData("silly", "sillier", 2)]
    [InlineData("narrow", "narrower", 2)]
    [InlineData("skinny", "skinnier", 2)]
    [InlineData("simple", "simpler", 2)]
    [InlineData("shallow", "shallower", 2)]
    [InlineData("gentle", "gentler", 2)]
    [InlineData("shiny", "shinier", 2)]
    [InlineData("humble", "humbler", 2)]
    [InlineData("sincere", "more sincere", 2)]
    [InlineData("salty", "saltier", 2)]
    [InlineData("bitter", "bitterer", 2)]
    [InlineData("busy", "busier", 2)]
    [InlineData("peaceful", "more peaceful", 2)]
    [InlineData("happy", "happier", 2)]
    [InlineData("tired", "more tired", 2)]
    [InlineData("ugly", "uglier", 2)]
    [InlineData("thirsty", "thirstier", 2)]
    [InlineData("tiny", "tinier", 2)]
    [InlineData("worldly", "worldlier", 2)]
    [InlineData("worthy", "worthier", 2)]
    [InlineData("wealthy", "wealthier", 2)]
    [InlineData("windy", "windier", 2)]
    [InlineData("tasty", "tastier", 2)]
    [InlineData("sorry", "sorrier", 2)]
    [InlineData("spicy", "spicier", 2)]
    [InlineData("smelly", "smellier", 2)]
    [InlineData("smoky", "smokier", 2)]
    [InlineData("sweaty", "sweatier", 2)]
    [InlineData("itchy", "itchier", 2)]
    [InlineData("stingy", "stingier", 2)]
    [InlineData("sunny", "sunnier", 2)]
    [InlineData("rusty", "rustier", 2)]
    [InlineData("juicy", "juicier", 2)]
    [InlineData("dirty", "dirtier", 2)]
    [InlineData("lazy", "lazier", 2)]
    [InlineData("icy", "icier", 2)]
    [InlineData("early", "earlier", 2)]
    [InlineData("dusty", "dustier", 2)]
    [InlineData("hungry", "hungrier", 2)]
    [InlineData("curvy", "curvier", 2)]
    [InlineData("messy", "messier", 2)]
    [InlineData("curly", "curlier", 2)]
    [InlineData("lovely", "lovelier", 2)]
    [InlineData("likely", "likelier", 2)]
    [InlineData("lively", "livelier", 2)]
    [InlineData("lonely", "lonelier", 2)]
    [InlineData("gloomy", "gloomier", 2)]
    [InlineData("greasy", "greasier", 2)]
    [InlineData("fancy", "fancier", 2)]
    [InlineData("funny", "funnier", 2)]
    [InlineData("flaky", "flakier", 2)]
    [InlineData("friendly", "friendlier", 2)]
    [InlineData("filthy", "filthier", 2)]
    [InlineData("healthy", "healthier", 2)]
    [InlineData("heavy", "heavier", 2)]
    [InlineData("easy", "easier", 2)]
    [InlineData("handy", "handier", 2)]
    [InlineData("greedy", "greedier", 2)]
    [InlineData("guilty", "guiltier", 2)]
    [InlineData("hairy", "hairier", 2)]
    [InlineData("modern", "more modern", 2)]
    [InlineData("cloudy", "cloudier", 2)]
    [InlineData("clumsy", "clumsier", 2)]
    [InlineData("chubby", "chubbier", 2)]
    [InlineData("classy", "classier", 2)]
    [InlineData("polite", "politer", 2)]
    [InlineData("crazy", "crazier", 2)]
    [InlineData("pretty", "prettier", 2)]
    [InlineData("clever", "cleverer", 2)]
    [InlineData("roomy", "roomier", 2)]
    [InlineData("risky", "riskier", 2)]
    [InlineData("pleasant", "more pleasant", 2)]
    [InlineData("careful", "more careful", 2)]
    [InlineData("bossy", "bossier", 2)]
    [InlineData("chewy", "chewier", 2)]
    [InlineData("thoughtful", "more thoughtful", 2)]
    [InlineData("bloody", "bloodier", 2)]
    [InlineData("oily", "oilier", 2)]
    [InlineData("needy", "needier", 2)]
    [InlineData("nasty", "nastier", 2)]
    [InlineData("crispy", "crispier", 2)]
    [InlineData("naughty", "naughtier", 2)]
    [InlineData("crunchy", "crunchier", 2)]
    [InlineData("cruel", "more cruel", 2)]
    [InlineData("deadly", "deadlier", 2)]
    [InlineData("creamy", "creamier", 2)]
    [InlineData("creepy", "creepier", 2)]
    [InlineData("noisy", "noisier", 2)]
    [InlineData("generous", "more generous", 3)]
    [InlineData("difficult", "more difficult", 3)]
    [InlineData("expensive", "more expensive", 3)]
    [InlineData("important", "more important", 3)]
    [InlineData("popular", "more popular", 3)]
    [InlineData("interesting", "more interesting", 4)]
    [InlineData("intelligent", "more intelligent", 4)]
    [InlineData("beautiful", "more beautiful", 5)]
    public void From_Should_ReturnCorrectComparativeForm(string oneSyllableAdjective, string correct, int syllablesCount)
    {
        // Arrange
        var comparisonAdjectiveText = new Text(oneSyllableAdjective);
        var syllablesCountObj = new SyllablesCount(syllablesCount);
        
        // Act
        var comparativeForm = ComparativeForm.From(comparisonAdjectiveText, syllablesCountObj);

        // Assert
        comparativeForm.Value.Should().Be(correct);
    }
}
