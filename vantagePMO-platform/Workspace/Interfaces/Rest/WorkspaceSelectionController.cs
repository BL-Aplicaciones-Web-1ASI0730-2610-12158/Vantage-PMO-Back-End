using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Workspace.Domain.Model.Aggregates;
using vantagePMO_platform.Workspace.Domain.Repositories;
using vantagePMO_platform.Workspace.Interfaces.Rest.Resources;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Workspace.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/workspace-selection")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Workspace segment selection")]
public class WorkspaceSelectionController(
    IWorkspaceSelectionRepository workspaceSelectionRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get workspace selection by user id", OperationId = "GetWorkspaceSelection")]
    [SwaggerResponse(StatusCodes.Status200OK, "Workspace selection found.", typeof(WorkspaceSelectionResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Workspace selection not found.")]
    public async Task<IActionResult> GetByUserId([FromQuery] int? userId, CancellationToken cancellationToken)
    {
        if (userId is null or <= 0)
            return BadRequest("Query parameter 'userId' is required.");

        var selection = await workspaceSelectionRepository.FindByUserIdAsync(userId.Value, cancellationToken);
        if (selection is null)
            return NotFound();

        return Ok(ToResource(selection));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Save workspace selection", OperationId = "CreateWorkspaceSelection")]
    [SwaggerResponse(StatusCodes.Status201Created, "Workspace selection saved.", typeof(WorkspaceSelectionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid workspace selection data.")]
    public async Task<IActionResult> Create(
        [FromBody] CreateWorkspaceSelectionResource resource,
        CancellationToken cancellationToken)
    {
        if (resource.UserId <= 0 || string.IsNullOrWhiteSpace(resource.SelectedRole))
            return BadRequest("userId and selectedRole are required.");

        try
        {
            var existing = await workspaceSelectionRepository.FindByUserIdAsync(resource.UserId, cancellationToken);
            if (existing is not null)
            {
                existing.UpdateRole(resource.SelectedRole);
                workspaceSelectionRepository.Update(existing);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Ok(ToResource(existing));
            }

            var selection = new WorkspaceSelection(resource.UserId, resource.SelectedRole);
            await workspaceSelectionRepository.AddAsync(selection, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return CreatedAtAction(
                nameof(GetByUserId),
                new { userId = selection.UserId },
                ToResource(selection));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    private static WorkspaceSelectionResource ToResource(WorkspaceSelection selection) =>
        new(selection.Id, selection.UserId, selection.SelectedRole);
}
