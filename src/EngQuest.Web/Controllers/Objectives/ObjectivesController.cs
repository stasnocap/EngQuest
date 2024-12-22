using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EngQuest.Application.Objectives.CompleteObjective;
using EngQuest.Application.Objectives.GetRandomObjective;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Objectives;

namespace EngQuest.Web.Controllers.Objectives;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/objectives")]
public class ObjectivesController(ISender _sender) : ControllerBase
{
    [HttpGet("random/{questId}")]
    public async Task<IActionResult> Random(int questId)
    {
        var query = new GetRandomObjectiveQuery(questId);

        Result<ObjectiveResponse> result = await _sender.Send(query, HttpContext.RequestAborted);

        return Ok(result.Value);
    }
    
    [HttpGet("{objectiveId}/complete")]
    public async Task<IActionResult> Complete(int objectiveId, int questId, string answer)
    {
        var command = new CompleteObjectiveCommand(objectiveId, questId, answer);

        Result<CompleteObjectiveResponse> result = await _sender.Send(command, HttpContext.RequestAborted);

        return Ok(result.Value);
    }
}
