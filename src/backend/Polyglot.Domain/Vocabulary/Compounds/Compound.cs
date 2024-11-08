using Polyglot.Domain.Abstractions;
using Polyglot.Domain.Shared;

namespace Polyglot.Domain.Vocabulary.Compounds;

public sealed class Compound
{
    public int Id { get; init; }
    public Text Text { get; }
    public CompoundType Type { get; }

    public Compound(Text text, CompoundType type)
    {
        Text = text;
        Type = type;
    }
    
    // ReSharper disable once UnusedMember.Local
    private Compound()
    {
    }
}
