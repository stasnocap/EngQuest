using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Dapper;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Extensions;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Objectives;
using EngQuest.Domain.Quests;
using EngQuest.Domain.Vocabulary;

namespace EngQuest.Application.Objectives.GetRandomObjective;

public class GetRandomObjectiveQueryHandler(
    ISqlConnectionFactory _sqlConnectionFactory,
    IVocabularyRepository _vocabularyRepository) : IQueryHandler<GetRandomObjectiveQuery, ObjectiveResponse>
{
    private const int WordGroupSize = 6;
    private const int RightAnswerCount = 1;
    private const int RandomWordsCount = WordGroupSize - RightAnswerCount;

    public async Task<Result<ObjectiveResponse>> Handle(GetRandomObjectiveQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection dbConnection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                        SELECT o.id, o.rus_phrase, w.number AS number, w.text AS text, w.type as type
                        FROM objectives o
                        INNER JOIN words AS w ON w.objective_id = o.id
                        WHERE o.id IN (
                            SELECT objective_id 
                            FROM objective_quest_ids i 
                            WHERE i.quest_id = @QuestId
                            ORDER BY random()
                            LIMIT 1
                        )
                     """;

        ObjectiveDto? randomObjective  = null;
        await dbConnection.QueryAsync<ObjectiveDto, WordDto, ObjectiveDto>(sql, (obj, word) =>
        {
            randomObjective ??= obj;
            randomObjective.Words.Add(word);
            return obj;
        }, new { request.QuestId }, splitOn: "number");

        if (randomObjective is null)
        {
            return Result.Failure<ObjectiveResponse>(QuestErrors.NotFound);
        }

        List<string[]> wordGroups = [];

        foreach (WordDto wordDto in randomObjective.Words.OrderBy(x => x.Number))
        {
            var word = new Word(new WordNumber(wordDto.Number), new Domain.Shared.Text(wordDto.Text), wordDto.Type);

            List<string> words = await _vocabularyRepository.GetRandomAsync(word, RandomWordsCount, dbConnection, cancellationToken);

            WordDecoratorService.Decorate(wordDto.Text, words);

            words.Insert(Random.Shared.Next(words.Count), word.Text.Value.ToLower(CultureInfo.InvariantCulture));

            wordGroups.Add([.. words]);
        }

        wordGroups.Shuffle(count: 4);

        return new ObjectiveResponse
        {
            ObjectiveId = randomObjective.Id,
            QuestId = request.QuestId,
            RusPhrase = randomObjective.RusPhrase,
            WordGroups = [.. wordGroups],
        };
    }

    [SnakeCaseMapping]
    public class ObjectiveDto
    {
        public required int Id { get; init; }
        public required string RusPhrase { get; init; }
        public HashSet<WordDto> Words { get; init; } = [];
    }

    [SnakeCaseMapping]
    public class WordDto
    {
        public required int Number { get; init; }
        public required string Text { get; init; }
        public required WordType Type { get; init; }
    }
}