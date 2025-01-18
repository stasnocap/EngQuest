using System.Data;
using Dapper;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Domain.Objectives;
using EngQuest.Domain.Vocabulary.ModalVerbs;
using EngQuest.Infrastructure.Data;

namespace EngQuest.Infrastructure.Repositories.Vocabulary;

public static class ModalVerbRepository
{
    public static async Task<List<string>> GetRandomModalVerbsAsync(Word word, int count, IDbConnection dbConnection)
    {
        string wordText = word.Text.GetWord();

        const string sql = """
                           WITH target AS (SELECT id, text, full_negative_form, short_negative_form
                                           FROM modal_verbs
                                           WHERE text = @Text OR full_negative_form = @Text OR short_negative_form = @Text
                                           LIMIT 1),
                           random AS (SELECT text, full_negative_form, short_negative_form
                                      FROM modal_verbs
                                      WHERE (SELECT COUNT(*) FROM target) = 0 OR id != (SELECT id FROM target)
                                      ORDER BY random()
                                      LIMIT @Count)
                           SELECT text, full_negative_form, short_negative_form FROM random
                           UNION ALL
                           SELECT text, full_negative_form, short_negative_form FROM target
                           """;

        var modalVerbs = (await dbConnection.QueryAsync<ModalVerbDto>(sql, new { Text = wordText, Count = count })).ToList();

        ModalVerbDto? target = null;
        if (modalVerbs.Count > 0)
        {
            ModalVerbDto maybeTarget = modalVerbs[^1];

            if (maybeTarget.Text == wordText || maybeTarget.FullNegativeForm == wordText || maybeTarget.ShortNegativeForm == wordText)
            {
                target = maybeTarget;
                modalVerbs.Remove(maybeTarget);
            }
        }
        
        if (target is not null)
        {
            if (wordText == target.Text)
            {
                return modalVerbs.Select(mv => mv.Text).ToList();
            }

            if (wordText == target.FullNegativeForm)
            {
                return modalVerbs.Select(mv => mv.FullNegativeForm).ToList();
            }

            if (wordText == target.ShortNegativeForm)
            {
                return modalVerbs.Select(mv => mv.ShortNegativeForm).ToList();
            }
        }
        else
        {
            if (FullNegativeForm.Is(word.Text))
            {
                return modalVerbs.Select(mv => mv.FullNegativeForm).ToList();
            }

            if (ShortNegativeForm.Is(word.Text))
            {
                return modalVerbs.Select(mv => mv.ShortNegativeForm).ToList();
            }

            return modalVerbs.Select(mv => mv.Text).ToList();
        }

        throw new ApplicationException();
    }
    
    [SnakeCaseMapping]
    public class ModalVerbDto
    {
        public required string Text { get; init; }

        public required string FullNegativeForm { get; init; }

        public required string ShortNegativeForm { get; init; }
    };
}
