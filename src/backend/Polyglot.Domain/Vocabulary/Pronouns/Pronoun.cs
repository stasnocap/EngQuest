using Polyglot.Domain.Abstractions;
using Polyglot.Domain.Shared;

namespace Polyglot.Domain.Vocabulary.Pronouns;

public sealed class Pronoun
{
    public int Id { get; init; }
    public Text Text { get; }
    public PronounType Type { get; }

    public Pronoun(Text text, PronounType type)
    {
        Text = text;
        Type = type;
    }

    // ReSharper disable once UnusedMember.Local
    private Pronoun()
    {
    }
}
