using EngQuest.Domain.Shared;

namespace EngQuest.Domain.Vocabulary.ComparisonAdjectives;

public sealed record ComparativeForm(string Value)
{
    public static implicit operator string(ComparativeForm comparativeForm) => comparativeForm.Value;
    
    public static bool Is(Text text)
    {
        return text.Value.StartsWith("more", StringComparison.InvariantCulture) || text.Value.EndsWith("er", StringComparison.InvariantCulture);
    }
    
    public static bool Is(string text)
    {
        return text.StartsWith("more", StringComparison.InvariantCulture) || text.EndsWith("er", StringComparison.InvariantCulture);
    }

    public static ComparativeForm From(Text comparisonAdjectiveText, SyllablesCount count)
    {
        string value = GenerateComparativeForm(comparisonAdjectiveText, count);

        return new ComparativeForm(value);
    }

    private static string GenerateComparativeForm(Text text, SyllablesCount count)
    {
        string textValue = text.Value;
        return count.Value switch
        {
            1 when textValue.EndsWith('e') => textValue + 'r',
            1 when textValue[^1] != 'w' && Letters.Consonants.Contains(textValue[^1]) && Letters.Vowels.Contains(textValue[^2]) && !Letters.Vowels.Contains(textValue[^3]) => textValue + textValue[^1] + "er",
            1 => textValue + "er",
            2 when textValue == "polite" => "politer",
            2 when textValue.EndsWith('y') => textValue[..^1] + "ier",
            2 when textValue.EndsWith("le", StringComparison.InvariantCulture) => textValue + "r",
            2 when textValue.EndsWith("er", StringComparison.InvariantCulture) || textValue.EndsWith("ow", StringComparison.InvariantCulture) => textValue + "er",
            2 => "more " + textValue,
            _ => "more " + textValue
        };
    }
}
