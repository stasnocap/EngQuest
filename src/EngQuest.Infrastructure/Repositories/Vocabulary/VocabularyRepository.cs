using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Objectives;
using EngQuest.Infrastructure.Data;

namespace EngQuest.Infrastructure.Repositories.Vocabulary;

public class VocabularyRepository : IVocabularyRepository
{
    public async Task<List<string>> GetRandomAsync(Word word, int count, IDbConnection dbConnection, CancellationToken cancellationToken)
    {
        switch (word.Type)
        {
            case WordType.Adjective:
            case WordType.City:
            case WordType.Determiner:
            case WordType.Language:
            case WordType.Preposition:
            case WordType.QuestionWord:
                {
                    string wordText = word.Text.GetWord();

                    string tableName = TableNames.GetTableName(word.Type);

                    string sql = $"""
                                  SELECT text FROM {tableName}
                                  WHERE text != @Word
                                  ORDER BY random()
                                  LIMIT @Count
                                  """;

                    IEnumerable<string> words = await dbConnection.QueryAsync<string>(sql, new { Word = wordText, Count = count });

                    return words.ToList();
                }
            case WordType.Adverb:
            case WordType.Compound:
            case WordType.Pronoun:
                {
                    string wordText = word.Text.GetWord();

                    string tableName = TableNames.GetTableName(word.Type);

                    string sql = $"""
                                  WITH target AS (SELECT id, type
                                               FROM {tableName}
                                               WHERE text = @Text
                                               LIMIT 1)
                                               
                                  SELECT text
                                  FROM {tableName} AS x
                                  WHERE (SELECT COUNT(*) FROM target) = 0
                                     OR (x.type = (SELECT type FROM target) AND x.id != (SELECT id FROM target))
                                  ORDER BY random()
                                  LIMIT @Count;
                                  """;

                    IEnumerable<string> words = await dbConnection.QueryAsync<string>(sql, new { Count = count, Text = wordText });

                    return words.ToList();
                }
            case WordType.ComparisonAdjective:
                return await ComparisonAdjectiveRepository.GetRandomComparisonAdjectivesAsync(word, count, dbConnection);
            case WordType.LetterNumber:
                return await LetterNumberRepository.GetRandomLetterNumbersAsync(word, count, dbConnection);
            case WordType.ModalVerb:
                return await ModalVerbRepository.GetRandomModalVerbsAsync(word, count, dbConnection);
            case WordType.Noun:
                return await NounRepository.GetRandomNounsAsync(word, count, dbConnection);
            case WordType.PrimaryVerb:
                return await PrimaryVerbRepository.GetRandomPrimaryVerbsAsync(word, count, dbConnection);
            case WordType.Verb:
                return await VerbRepository.GetRandomVerbsAsync(word, count, dbConnection);
            case WordType.NumberWithNoun:
                return await NumberWithNounRepository.GetRandomNumberWithNounsAsync(word, count, dbConnection);
            case WordType.None:
            default:
                throw new ApplicationException();
        }
    }
}
