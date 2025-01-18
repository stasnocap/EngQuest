using System.Data;
using Dapper;
using EngQuest.Domain.Objectives;
using Microsoft.EntityFrameworkCore;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.LetterNumbers;
using EngQuest.Infrastructure.Extensions;

namespace EngQuest.Infrastructure.Repositories.Vocabulary;

public static class LetterNumberRepository
{
    public static async Task<List<string>> GetRandomLetterNumbersAsync(Word word, int count, IDbConnection dbConnection)
    {
        string wordText = word.Text.GetWord();
        
        const string sql = """
                            WITH target AS (SELECT id, text, number
                                            FROM letter_numbers
                                            WHERE text = @Text
                                            LIMIT 1),
                            random AS (SELECT text, number
                                       FROM letter_numbers
                                       WHERE (SELECT COUNT(*) FROM target) = 0 
                                              OR (id != (SELECT id FROM target) 
                                              AND number BETWEEN (SELECT number FROM target) - (@Count / 2) 
                                                         AND (SELECT number FROM target) + (@Count / 2) + 1)           
                                       ORDER BY random()
                                       LIMIT @Count)
                            SELECT text FROM random
                            """;
        
        IEnumerable<string> letterNumbers = await dbConnection.QueryAsync<string>(sql, new { Text = wordText, Count = count });

        return letterNumbers.ToList();
    }
}
