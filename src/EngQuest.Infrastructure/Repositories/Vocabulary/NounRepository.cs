using System.Data;
using Dapper;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Domain.Objectives;
using EngQuest.Domain.Vocabulary.Nouns;
using EngQuest.Infrastructure.Data;

namespace EngQuest.Infrastructure.Repositories.Vocabulary;

public class NounRepository()
{
    public async Task<List<string>> GetRandomNounsAsync(Word word, int count, IDbConnection dbConnection)
    {
        string wordText = word.Text.GetWord();

        const string sql = """
                           WITH target AS (SELECT id, text, plural_form
                                           FROM nouns
                                           WHERE text = @Text OR plural_form = @Text
                                           LIMIT 1),
                           random AS (SELECT text, plural_form
                                      FROM nouns
                                      WHERE (SELECT COUNT(*) FROM target) = 0 OR id != (SELECT id FROM target)
                                      ORDER BY random()
                                      LIMIT @Count)
                           SELECT text, plural_form FROM random
                           UNION ALL
                           SELECT text, plural_form FROM target
                           """;

        var nouns = (await dbConnection.QueryAsync<NounDto>(sql, new { Text = wordText, Count = count })).ToList();

        NounDto? target = null;
        if (nouns.Count > 0)
        {
            NounDto maybeTarget = nouns[^1];

            if (maybeTarget.Text == wordText || maybeTarget.PluralForm == wordText)
            {
                target = maybeTarget;
                nouns.Remove(maybeTarget);
            }
        }
        
        if (target is not null)
        {
            if (wordText == target.Text)
            {
                return nouns.Select(n => n.Text).ToList();
            }

            if (wordText == target.PluralForm)
            {
                return nouns.Select(n => n.PluralForm).ToList();
            }
        }
        else
        {
            if (PluralForm.Is(word.Text))
            {
                return nouns.Select(n => n.PluralForm).ToList();
            }

            return nouns.Select(n => n.Text).ToList();
        }

        throw new ApplicationException();
    }
    
    [SnakeCaseMapping]
    public class NounDto
    {
        public required string Text { get; init; }

        public required string PluralForm { get; init; }
    };
}
