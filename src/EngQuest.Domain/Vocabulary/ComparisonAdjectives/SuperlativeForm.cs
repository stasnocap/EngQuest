using EngQuest.Domain.Shared;

namespace EngQuest.Domain.Vocabulary.ComparisonAdjectives;

public sealed record SuperlativeForm(string Value)
{
    public static implicit operator string(SuperlativeForm superlativeForm) => superlativeForm.Value;
    
    public static bool Is(Text text)
    {
        return text.Value.StartsWith("most", StringComparison.InvariantCulture) || text.Value.EndsWith("est", StringComparison.InvariantCulture);
    }
    
    public static bool Is(string text)
    {
        return text.StartsWith("most", StringComparison.InvariantCulture) || text.EndsWith("est", StringComparison.InvariantCulture);
    }

    public static SuperlativeForm From(Text comparisonAdjectiveText, SyllablesCount count)
    {
        string value = GenerateSuperlativeForm(comparisonAdjectiveText, count);

        return new SuperlativeForm(value);
    }

    private static string GenerateSuperlativeForm(Text text, SyllablesCount count)
    {
        string textValue = text.Value;
        return count.Value switch
        {
            1 when textValue.EndsWith('e') => textValue + "st",
            1 when textValue[^1] != 'w' && Letters.Consonants.Contains(textValue[^1]) && Letters.Vowels.Contains(textValue[^2]) && !Letters.Vowels.Contains(textValue[^3]) => textValue + textValue[^1] + "est",
            1 => textValue + "est",
            2 when textValue == "polite" => "politest",
            2 when textValue.EndsWith('y') => textValue[..^1] + "iest",
            2 when textValue.EndsWith("le", StringComparison.InvariantCulture) => textValue + "st",
            2 when textValue.EndsWith("er", StringComparison.InvariantCulture) || textValue.EndsWith("ow", StringComparison.InvariantCulture) => textValue + "est",
            2 => "most " + textValue,
            _ => "most " + textValue
        };
    }
}
