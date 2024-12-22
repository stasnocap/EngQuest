using EngQuest.Domain.Shared;

namespace EngQuest.Domain.Vocabulary.Nouns;

public sealed record PluralForm(string Value)
{
    public static implicit operator string(PluralForm pluralForm) => pluralForm.Value;
    
    public static bool Is(Text singularNoun)
    {
        return singularNoun.Value.EndsWith('s');
    }

    public static PluralForm From(Text singularNoun)
    {
        string pluralFormStr = GeneratePluralForm(singularNoun.Value);

        return new PluralForm(pluralFormStr);
    }

    private static string GeneratePluralForm(string text)
    {
        if (text.EndsWith("on", StringComparison.InvariantCulture) && text[^3] != 'o')
        {
            return text[..^2] + "a";
        }

        if (text.EndsWith("is", StringComparison.InvariantCulture))
        {
            return text[..^2] + "es";
        }

        if (text.EndsWith('f')
            && text != "roof"
            && text != "belief"
            && text != "chef"
            && text != "chief")
        {
            return text[..^1] + "ves";
        }

        if (text.EndsWith("fe", StringComparison.InvariantCulture))
        {
            return text[..^2] + "ves";
        }

        if (text.EndsWith('y') && Letters.Consonants.Contains(text[^2]))
        {
            return text[..^1] + "ies";
        }

        if (text.EndsWith('s')
            || text.EndsWith("ss", StringComparison.InvariantCulture)
            || text.EndsWith("sh", StringComparison.InvariantCulture)
            || text.EndsWith("ch", StringComparison.InvariantCulture)
            || text.EndsWith('x')
            || text.EndsWith('z')
            || text.EndsWith("es", StringComparison.InvariantCulture)
            || text.EndsWith('o')
            && text != "photo"
            && text != "piano"
            && text != "halo")
        {
            return text + "es";
        }

        return text + "s";
    }
}
