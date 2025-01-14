using System.Data;
using Dapper;
using EngQuest.Application.Abstractions.Repositories.Objectives;
using EngQuest.Domain.Objectives;
using Microsoft.EntityFrameworkCore;
using static EngQuest.Application.Objectives.GetRandomObjective.GetRandomObjectiveQueryHandler;

namespace EngQuest.Infrastructure.Repositories;

public class ObjectiveRepository(ApplicationDbContext dbContext) : Repository<Objective>(dbContext), IObjectiveRepository
{
    public async Task<ObjectiveDto> GetRandomAsync(int questId, IDbConnection dbConnection)
    {
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
        }, new { questId }, splitOn: "number");

        return randomObjective;
    }

    public Task<Objective?> GetByIdAsync(int objectiveId, int questId, CancellationToken cancellationToken)
    {
        return DbContext
            .Set<Objective>()
            .FirstOrDefaultAsync(x => x.Id == objectiveId && x.QuestIds.Any(y => y.Value == questId), cancellationToken);
    }
}
