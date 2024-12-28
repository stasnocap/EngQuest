using FluentAssertions;
using EngQuest.Domain.Shared;

namespace EngQuest.Domain.UnitTests.Shared;

public  class TextTests
{
    [Theory]
    [InlineData("Didn't", "didn't")]
    [InlineData("More appropriate.", "more appropriate")]
    [InlineData(",Did not", "did not")]
    [InlineData("do not,", "do not")]
    public void GetWord_Should_ReturnWordFromText(string textStr, string correct)
    {
        // Arrange
        var text = new Text(textStr);
        
        // Act
        string result = text.GetWord();
        
        // Assert
        result.Should().Be(correct);
    }
}
