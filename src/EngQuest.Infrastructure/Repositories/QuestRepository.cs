using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using EngQuest.Domain.Quests;
using EngQuest.Application.Abstractions.Repositories;
using System.Data;
using EngQuest.Application.Quests.GetQuests;
using Dapper;

namespace EngQuest.Infrastructure.Repositories;

internal sealed class QuestRepository(ApplicationDbContext dbContext) : Repository<Quest>(dbContext), IQuestRepository
{
    public async Task<List<QuestResponse>> GetAllAsync(IDbConnection dbConnection)
    {
        const string sql = """
                            SELECT id, name
                            FROM quests
                            ORDER BY id asc;
                           """;

        IEnumerable<QuestResponse> query = await dbConnection.QueryAsync<QuestResponse>(sql);

        return [..query];
    }
}
