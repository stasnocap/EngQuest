using System.Data;
using Dapper;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Domain.Objectives;
using EngQuest.Domain.Vocabulary.ComparisonAdjectives;
using EngQuest.Infrastructure.Data;

namespace EngQuest.Infrastructure.Repositories.Vocabulary;

public static class ComparisonAdjectiveRepository
{
    public async static Task<List<string>> GetRandomComparisonAdjectivesAsync(Word word, int count, IDbConnection dbConnection)
    {
        string wordText = word.Text.GetWord();

        const string sql = """
                           WITH target AS (SELECT id, text, comparative_form, superlative_form
                                           FROM comparison_adjectives
                                           WHERE text = @Text OR comparative_form = @Text OR superlative_form = @Text
                                           LIMIT 1),
                           random AS (SELECT text, comparative_form, superlative_form
                                      FROM comparison_adjectives
                                      WHERE (SELECT COUNT(*) FROM target) = 0 OR id != (SELECT id FROM target)
                                      ORDER BY random()
                                      LIMIT @Count)
                           SELECT text, comparative_form, superlative_form FROM random
                           UNION ALL
                           SELECT text, comparative_form, superlative_form FROM target
                           """;

        var comparisonAdjectives = (await dbConnection.QueryAsync<ComparisonAdjectiveDto>(sql, new { Text = wordText, Count = count })).ToList();

        ComparisonAdjectiveDto? target = null;
        if (comparisonAdjectives.Count > 0)
        {
            ComparisonAdjectiveDto maybeTarget = comparisonAdjectives[^1];

            if (maybeTarget.Text == wordText || maybeTarget.ComparativeForm == wordText || maybeTarget.SuperlativeForm == wordText)
            {
                target = maybeTarget;
                comparisonAdjectives.Remove(maybeTarget);
            }
        }
        
        if (target is not null)
        {
            if (wordText == target.Text)
            {
                return comparisonAdjectives.Select(ca => ca.Text).ToList();
            }

            if (wordText == target.ComparativeForm)
            {
                return comparisonAdjectives.Select(ca => ca.ComparativeForm).ToList();
            }

            if (wordText == target.SuperlativeForm)
            {
                return comparisonAdjectives.Select(ca => ca.SuperlativeForm).ToList();
            }
        }
        else
        {
            if (ComparativeForm.Is(wordText))
            {
                return comparisonAdjectives.Select(ca => ca.ComparativeForm).ToList();
            }

            if (SuperlativeForm.Is(wordText))
            {
                return comparisonAdjectives.Select(ca => ca.SuperlativeForm).ToList();
            }

            return comparisonAdjectives.Select(ca => ca.Text).ToList();
        }

        throw new ApplicationException();
    }

    [SnakeCaseMapping]
    public class ComparisonAdjectiveDto
    {
        public required string Text { get; init; }

        public required string ComparativeForm { get; init; }

        public required string SuperlativeForm { get; init; }
    };
}
