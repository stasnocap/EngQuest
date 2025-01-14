using System.Data;
using System.Text.RegularExpressions;
using EngQuest.Domain.Objectives;

namespace EngQuest.Infrastructure.Repositories.Vocabulary;

public static class NumberWithNounRepository
{
    public async static Task<List<string>> GetRandomNumberWithNounsAsync(Word word, int count, IDbConnection dbConnection)
    {
        List<string> nouns = await NounRepository.GetRandomNounsAsync(word, count, dbConnection);
        
        string? number = Regex.Match(word.Text.Value, @"\d+").Value;

        return nouns.Select(n => $"{number} {n}").ToList();
    }
}
