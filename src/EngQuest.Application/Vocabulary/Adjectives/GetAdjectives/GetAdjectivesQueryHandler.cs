using EngQuest.Application.Abstractions.Messaging;
using EngQuest.Application.Abstractions.Repositories;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary;
using EngQuest.Domain.Vocabulary.Adjectives;

namespace EngQuest.Application.Vocabulary.Adjectives.GetAdjectives;

public class GetAdjectivesQueryHandler(IAdjectiveRepository _repository) : IQueryHandler<GetAdjectivesQuery, PagedList<AdjectiveResponse>>
{
    public async Task<Result<PagedList<AdjectiveResponse>>> Handle(GetAdjectivesQuery request, CancellationToken cancellationToken)
    {
        if (request.Page <= 0 || request.PageSize <= 0)
        {
            return PagedList<AdjectiveResponse>.Empty();
        }
        
        PagedList<AdjectiveResponse> result = await _repository.GetPagedAsync<Adjective, AdjectiveResponse>(
            request.Page, 
            request.PageSize,
            request.SortColumn, 
            request.SortOrder,
            a => new AdjectiveResponse
            {
                Id = a.Id,
                Text = a.Text,
            },
            cancellationToken);

        return result;
    }
}
