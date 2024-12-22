using System.Data;
using System.Linq.Expressions;
using EngQuest.Domain.Objectives;
using EngQuest.Domain.Shared;

namespace EngQuest.Domain.Vocabulary;

public interface IVocabularyRepository
{
    Task<List<string>> GetRandomAsync(Word word, int count, IDbConnection dbConnection, CancellationToken cancellationToken);
}
