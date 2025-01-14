using System.Linq.Expressions;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.Adjectives;

namespace EngQuest.Application.Abstractions.Repositories;

public interface IAdjectiveRepository
{
    Task<Adjective?> GetByIdAsync(int id, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(Text text, CancellationToken cancellationToken);
    
    void Add(Adjective adjective);
    
    void Delete(Adjective adjective);
        
    Task<PagedList<TResult>> GetPagedAsync<TEntity, TResult>(
        int page, 
        int pageSize, 
        string? sortColumn, 
        string? sortOrder,
        Expression<Func<TEntity, TResult>> select, 
        CancellationToken cancellationToken);
}
