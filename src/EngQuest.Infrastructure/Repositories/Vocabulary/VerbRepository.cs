using EngQuest.Domain.Objectives;
using Microsoft.EntityFrameworkCore;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.Verbs;
using EngQuest.Infrastructure.Extensions;
using EngQuest.Infrastructure.Data;
using System.Data;
using Dapper;
using EngQuest.Application.Abstractions.Data;

namespace EngQuest.Infrastructure.Repositories.Vocabulary;

public class VerbRepository()
{
    public async Task<List<string>> GetRandomVerbsAsync(Word word, int count, IDbConnection dbConnection)
    {
        string wordText = word.Text.GetWord();

        const string sql = """
                    WITH target AS (SELECT id, text, past_form, past_participle_form, present_participle_form, third_person_form
                                    FROM verbs
                                    WHERE text = @Text OR past_form = @Text OR past_participle_form = @Text OR present_participle_form = @Text OR third_person_form = @Text
                                    LIMIT 1),
                    random AS (SELECT text, past_form, past_participle_form, present_participle_form, third_person_form
                                FROM verbs
                                WHERE (SELECT COUNT(*) FROM target) = 0 OR id != (SELECT id FROM target)
                                ORDER BY random()
                                LIMIT @Count)
                    SELECT text, past_form, past_participle_form, present_participle_form, third_person_form FROM random
                    UNION ALL
                    SELECT text, past_form, past_participle_form, present_participle_form, third_person_form FROM target
                    """;

        var verbs = (await dbConnection.QueryAsync<VerbDto>(sql, new { Text = wordText, Count = count })).ToList();

        VerbDto? target = null;
        if (verbs.Count > 0)
        {
            VerbDto maybeTarget = verbs[^1];

            if (maybeTarget.Text == wordText 
                || maybeTarget.PastForm == wordText 
                || maybeTarget.PastParticipleForm == wordText
                || maybeTarget.PresentParticipleForm == wordText
                || maybeTarget.ThirdPersonForm == wordText
                )
            {
                target = maybeTarget;
                verbs.Remove(maybeTarget);
            }
        }

        if (target is not null)
        {
            if (wordText == target.Text)
            {
                return verbs.Select(v => v.Text).ToList();
            }

            if (wordText == target.PastForm)
            {
                return verbs.Select(v => v.PastForm).ToList();
            }

            if (wordText == target.PastParticipleForm)
            {
                return verbs.Select(v => v.PastParticipleForm).ToList();
            }

            if (wordText == target.PresentParticipleForm)
            {
                return verbs.Select(v => v.PresentParticipleForm).ToList();
            }

            if (wordText == target.ThirdPersonForm)
            {
                return verbs.Select(v => v.ThirdPersonForm).ToList();
            }
        }
        else
        {
            if (PastForm.Is(word.Text))
            {
                return verbs.Select(v => v.PastForm).ToList();
            }

            if (PastParticipleForm.Is(word.Text))
            {
                return verbs.Select(v => v.PastParticipleForm).ToList();
            }

            if (PresentParticipleForm.Is(word.Text))
            {
                return verbs.Select(v => v.PresentParticipleForm).ToList();
            }

            if (ThirdPersonForm.Is(word.Text))
            {
                return verbs.Select(v => v.ThirdPersonForm).ToList();
            }

            return verbs.Select(v => v.Text).ToList();
        }

        throw new ApplicationException();
    }

    [SnakeCaseMapping]
    public class VerbDto
    {
        public required string Text { get; init; }
        public required string PastForm { get; init; }
        public required string PastParticipleForm { get; init; }
        public required string PresentParticipleForm { get; init; }
        public required string ThirdPersonForm { get; init; }
    }
}
