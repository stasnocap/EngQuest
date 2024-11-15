using EngQuest.Domain.Shared;

namespace EngQuest.Domain.Vocabulary.ModalVerbs;

public sealed record ShortNegativeForm(string Value)
{
    public static implicit operator string(ShortNegativeForm shortNegativeForm) => shortNegativeForm.Value;
    
    public static bool Is(Text text)
    {
        return text.Value.EndsWith("n't", StringComparison.InvariantCulture);
    }
}
