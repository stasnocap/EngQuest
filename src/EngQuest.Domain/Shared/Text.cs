using System.Globalization;
using System.Text.RegularExpressions;

namespace EngQuest.Domain.Shared;

public sealed record Text(string Value)
{
    public static implicit operator string(Text text) => text.Value;
    
    public string GetWord()
    {
        Match match = Regex.Match(Value, @"([mM]ore |[mM]ost |\d )?[a-zA-Z0-9'-]+( not\b)?");

        string word = match.Value.ToLower(CultureInfo.GetCultureInfo("en-US"));

        return word;
    }
}
