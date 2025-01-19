using EngQuest.Domain.Objectives;
using Microsoft.EntityFrameworkCore;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.PrimaryVerbs;
using EngQuest.Infrastructure.Extensions;
using EngQuest.Infrastructure.Data;
using System.Data;
using Dapper;
using System.Collections.Specialized;
using EngQuest.Application.Abstractions.Data;

namespace EngQuest.Infrastructure.Repositories.Vocabulary;

public static class PrimaryVerbRepository
{
    public static async Task<List<string>> GetRandomPrimaryVerbsAsync(Word word, int count, IDbConnection dbConnection)
    {
        string wordText = word.Text.GetWord();

        const string sql = """
              WITH target AS (SELECT pv.id, pv.text, past_form, past_participle_form, present_participle_form, third_person_form, fnf.text AS full_negative_form, snf.text AS short_negative_form, af.text AS additional_form
                              FROM primary_verbs pv
                              LEFT JOIN "full_negative_forms" fnf on fnf.primary_verb_id = pv.id
                              LEFT JOIN "short_negative_forms" snf on snf.primary_verb_id = pv.id
                              LEFT JOIN "additional_forms" af on af.primary_verb_id = pv.id
                              WHERE pv.text = @Text 
                                OR past_form = @Text 
                                OR past_participle_form = @Text 
                                OR present_participle_form = @Text 
                                OR third_person_form = @Text
                                OR fnf.text = @Text
                                OR snf.text = @Text
                                OR af.text = @Text),
              random AS (SELECT pv.id, pv.text, past_form, past_participle_form, present_participle_form, third_person_form, fnf.text AS full_negative_form, snf.text AS short_negative_form, af.text AS additional_form
                         FROM primary_verbs pv
                         LEFT JOIN "full_negative_forms" fnf on fnf.primary_verb_id = pv.id
                         LEFT JOIN "short_negative_forms" snf on snf.primary_verb_id = pv.id
                         LEFT JOIN "additional_forms" af on af.primary_verb_id = pv.id
                         WHERE (SELECT COUNT(*) FROM target) = 0 OR pv.id NOT IN (SELECT id FROM target)
                         ORDER BY random())
              SELECT id, text, past_form, past_participle_form, present_participle_form, third_person_form, full_negative_form, short_negative_form, additional_form FROM random
              UNION ALL
              SELECT id, text, past_form, past_participle_form, present_participle_form, third_person_form, full_negative_form, short_negative_form, additional_form FROM target
              """;

        Dictionary<int, PrimaryVerbDto> primaryVerbs = [];
        await dbConnection.QueryAsync<PrimaryVerbDto, string, string, string, PrimaryVerbDto>(sql, (primaryVerb, fullNegativeForm, shortNegativeForm, additionalForm) =>
        {
            primaryVerbs.TryAdd(primaryVerb.Id, primaryVerb);
            primaryVerbs[primaryVerb.Id].FullNegativeForms.Add(fullNegativeForm);
            primaryVerbs[primaryVerb.Id].ShortNegativeForms.Add(shortNegativeForm);
            primaryVerbs[primaryVerb.Id].AdditionalForms.Add(additionalForm);
            return primaryVerb;
        }, new { Text = wordText, Count = count },  splitOn: "full_negative_form,short_negative_form,additional_form");

        PrimaryVerbDto? target = null;
        if (primaryVerbs.Count > 0)
        {
            PrimaryVerbDto maybeTarget = primaryVerbs.Last().Value;

            if (maybeTarget.Text == wordText 
                || maybeTarget.PastForm == wordText 
                || maybeTarget.PastParticipleForm == wordText
                || maybeTarget.PresentParticipleForm == wordText
                || maybeTarget.ThirdPersonForm == wordText
                || maybeTarget.FullNegativeForms.Contains(wordText)
                || maybeTarget.ShortNegativeForms.Contains(wordText)
                || maybeTarget.AdditionalForms.Contains(wordText)
                )
            {
                target = maybeTarget;
                primaryVerbs.Remove(maybeTarget.Id);
            }
        }

        if (target is not null)
        {
            if (wordText == target.Text)
            {
                return GetTexts(primaryVerbs, count, target.Id);
            }

            if (wordText == target.PastForm)
            {
                var pastForms = primaryVerbs.Select(pv => pv.Value.PastForm).ToList();

                AddTextsIfNotFilledUp(pastForms, count, primaryVerbs, target.Id);

                return pastForms;
            }

            if (wordText == target.PastParticipleForm)
            {
                var pastParticipleForms = primaryVerbs.Select(pv => pv.Value.PastParticipleForm).ToList();

                AddTextsIfNotFilledUp(pastParticipleForms, count, primaryVerbs, target.Id);

                return pastParticipleForms;
            }

            if (wordText == target.PresentParticipleForm)
            {
                var presentParticipleForms = primaryVerbs.Select(pv => pv.Value.PresentParticipleForm).ToList();

                AddTextsIfNotFilledUp(presentParticipleForms, count, primaryVerbs, target.Id);

                return presentParticipleForms;
            }

            if (wordText == target.ThirdPersonForm)
            {
                var thirdPersonForms = primaryVerbs.Select(pv => pv.Value.ThirdPersonForm).ToList();

                AddTextsIfNotFilledUp(thirdPersonForms, count, primaryVerbs, target.Id);

                return thirdPersonForms;
            }

            if (target.FullNegativeForms.Contains(wordText))
            {
                var fullNegativeForms = target.FullNegativeForms
                    .Where(fnf => fnf != wordText)
                    .Select(fnf => fnf).ToList();

                if (fullNegativeForms.Count < count)
                {
                    fullNegativeForms.AddRange(primaryVerbs
                        .SelectMany(pv => pv.Value.FullNegativeForms)
                        .OrderBy(_ => Guid.NewGuid())
                        .Take(count - fullNegativeForms.Count));
                }

                return fullNegativeForms;
            }

            if (target.ShortNegativeForms.Contains(wordText))
            {
                var shortNegativeForms = target.ShortNegativeForms
                    .Where(fnf => fnf != wordText)
                    .ToList();

                if (shortNegativeForms.Count < count)
                {
                    shortNegativeForms.AddRange(primaryVerbs
                        .SelectMany(pv => pv.Value.ShortNegativeForms)
                        .OrderBy(_ => Guid.NewGuid())
                        .Take(count - shortNegativeForms.Count));
                }

                return shortNegativeForms;
            }

            if (target.AdditionalForms.Contains(wordText))
            {
                var additionalForms = target.AdditionalForms
                    .Where(fnf => fnf != wordText)
                    .Select(fnf => fnf).ToList();

                AddTextsIfNotFilledUp(additionalForms, count, primaryVerbs, target.Id);

                return additionalForms;
            }
        }

        return GetTexts(primaryVerbs, count);
    }

    private static void AddTextsIfNotFilledUp(List<string> forms, int count, Dictionary<int, PrimaryVerbDto> primaryVerbs, int targetId)
    {
        if (forms.Count < count)
        {
            forms.AddRange(primaryVerbs
                .Where(pv => pv.Key != targetId)
                .Select(pv => pv.Value.Text)
                .OrderBy(pv => Guid.NewGuid())
                .Take(count - forms.Count));
        }
    }

    private static List<string> GetTexts(Dictionary<int, PrimaryVerbDto> primaryVerbs, int count, int? targetId = null)
    {
        var texts = primaryVerbs.Select(pv => pv.Value.Text).ToList();

        if (texts.Count < count)
        {
            // fill up with the rest with past forms
            texts.AddRange(
                primaryVerbs
                .Where(pv => pv.Key != targetId)
                .Select(pv => pv.Value.PastForm)
                .OrderBy(pv => Guid.NewGuid())
                .Take(count - texts.Count)
            );
        }

        return texts;
    }

    [SnakeCaseMapping]
    public class PrimaryVerbDto
    {
        public required int Id { get; init; }
        public required string Text { get; init; }

        public required string PastForm { get; init; }

        public required string PastParticipleForm { get; init; }

        public required string PresentParticipleForm { get; init; }

        public required string ThirdPersonForm { get; init; }

        public HashSet<string> FullNegativeForms { get; init; } = [];

        public HashSet<string> ShortNegativeForms { get; init; } = [];

        public HashSet<string> AdditionalForms { get; init; } = [];
    };
}
