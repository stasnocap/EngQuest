using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using EngQuest.Application.Abstractions.Data;
using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Application.Abstractions.Repositories.Objectives;
using EngQuest.Application.Extensions;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Objectives;
using EngQuest.Domain.Quests;

namespace EngQuest.Application.Objectives.GetRandomObjective;

public class GetRandomObjectiveQueryHandler(
    ISqlConnectionFactory _sqlConnectionFactory,
    IObjectiveRepository objectiveRepository,
    IVocabularyRepository _vocabularyRepository) : IQueryHandler<GetRandomObjectiveQuery, ObjectiveResponse>
{
    private const int WordGroupSize = 6;
    private const int RightAnswerCount = 1;
    private const int RandomWordsCount = WordGroupSize - RightAnswerCount;

    public async Task<Result<ObjectiveResponse>> Handle(GetRandomObjectiveQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection dbConnection = _sqlConnectionFactory.CreateConnection();

        ObjectiveDto? randomObjective  = await objectiveRepository.GetRandomAsync(request.QuestId, dbConnection);

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
}