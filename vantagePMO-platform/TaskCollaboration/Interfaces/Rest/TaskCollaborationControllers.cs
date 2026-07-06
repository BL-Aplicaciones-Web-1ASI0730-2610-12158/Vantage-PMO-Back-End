using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Dashboard.Application.QueryServices;
using vantagePMO_platform.Dashboard.Domain.Model.Queries;
using vantagePMO_platform.Dashboard.Interfaces.Rest.Transform;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.TaskCollaboration.Application.CommandServices;
using vantagePMO_platform.TaskCollaboration.Application.QueryServices;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Queries;
using vantagePMO_platform.TaskCollaboration.Interfaces.Rest.Resources;
using vantagePMO_platform.TaskCollaboration.Interfaces.Rest.Transform;

namespace vantagePMO_platform.TaskCollaboration.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/boards")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Team boards")]
public class BoardsController(IBoardQueryService boardQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all boards", OperationId = "GetAllBoards")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var boards = await boardQueryService.Handle(new GetAllBoardsQuery(), cancellationToken);
        return Ok(TaskCollaborationResourceFromEntityAssembler.ToResources(boards));
    }
}

[AllowAnonymous]
[ApiController]
[Route("api/v1/board-members")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Board members")]
public class BoardMembersController(IBoardMemberQueryService boardMemberQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get board members by board", OperationId = "GetBoardMembers")]
    public async Task<IActionResult> GetByBoard([FromQuery] int boardId, CancellationToken cancellationToken)
    {
        var members = await boardMemberQueryService.Handle(new GetBoardMembersByBoardQuery(boardId), cancellationToken);
        return Ok(TaskCollaborationResourceFromEntityAssembler.ToResources(members));
    }
}

[AllowAnonymous]
[ApiController]
[Route("api/v1/tasks")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Team board tasks")]
public class CollaborationTasksController(
    ICollaborationTaskCommandService collaborationTaskCommandService,
    ICollaborationTaskQueryService collaborationTaskQueryService,
    IDashboardTaskQueryService dashboardTaskQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get dashboard or collaboration tasks", OperationId = "GetCollaborationTasks")]
    public async Task<IActionResult> GetAll(
        [FromQuery] int? boardId,
        [FromQuery] string? scope,
        CancellationToken cancellationToken)
    {
        if (string.Equals(scope, "collaboration", StringComparison.OrdinalIgnoreCase) || boardId.HasValue)
        {
            var tasks = await collaborationTaskQueryService.Handle(new GetCollaborationTasksQuery(boardId), cancellationToken);
            return Ok(TaskCollaborationResourceFromEntityAssembler.ToResources(tasks));
        }

        var dashboardTasks = await dashboardTaskQueryService.Handle(new GetAllDashboardTasksQuery(), cancellationToken);
        return Ok(DashboardTaskResourceFromEntityAssembler.ToResourcesFromEntities(dashboardTasks));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a collaboration task", OperationId = "CreateCollaborationTask")]
    public async Task<IActionResult> Create(
        [FromBody] CreateCollaborationTaskResource resource,
        CancellationToken cancellationToken)
    {
        var command = CollaborationTaskCommandFromResourceAssembler.ToCreateCommand(resource);
        var task = await collaborationTaskCommandService.CreateAsync(command, cancellationToken);
        if (task is null)
            return BadRequest("Could not create task.");

        return CreatedAtAction(nameof(GetAll), new { id = task.Id },
            TaskCollaborationResourceFromEntityAssembler.ToResource(task));
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update a collaboration task", OperationId = "UpdateCollaborationTask")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateCollaborationTaskResource resource,
        CancellationToken cancellationToken)
    {
        var command = CollaborationTaskCommandFromResourceAssembler.ToUpdateCommand(id, resource);
        var task = await collaborationTaskCommandService.UpdateAsync(command, cancellationToken);
        if (task is null)
            return NotFound($"Task {id} was not found.");

        return Ok(TaskCollaborationResourceFromEntityAssembler.ToResource(task));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete a collaboration task", OperationId = "DeleteCollaborationTask")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await collaborationTaskCommandService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"Task {id} was not found.");
    }
}
