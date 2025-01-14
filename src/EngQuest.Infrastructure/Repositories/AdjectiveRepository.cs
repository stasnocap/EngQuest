using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.Adjectives;
using EngQuest.Application.Abstractions.Repositories;

namespace EngQuest.Infrastructure.Repositories;

internal sealed class AdjectiveRepository(ApplicationDbContext dbContext) : Repository<Adjective>(dbContext), IAdjectiveRepository
{
    public Task<bool> ExistsAsync(Text text, CancellationToken cancellationToken)
    {
        return DbContext.Set<Adjective>().AnyAsync(x => x.Text == text, cancellationToken);
    }

    public Task<PagedList<TResult>> GetPagedAsync<TEntity, TResult>(int page, int pageSize, string? sortColumn, string? sortOrder, Expression<Func<TEntity, TResult>> select, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
