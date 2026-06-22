using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Projects.Domain.Model;
using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.Queries;
using vantagePMO_platform.Projects.Application.CommandServices;
using vantagePMO_platform.Projects.Application.QueryServices;
using vantagePMO_platform.Projects.Interfaces.Rest.Resources;
using vantagePMO_platform.Projects.Interfaces.Rest.Transform;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Interfaces.Rest.ProblemDetails;
using vantagePMO_platform.Shared.Resources.Errors;

namespace vantagePMO_platform.Projects.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Project management")]
public class ProjectsController(
    IProjectCommandService projectCommandService,
    IProjectQueryService projectQueryService,
    ProblemDetailsFactory problemDetailsFactory,
    IStringLocalizer<ErrorMessages> localizer,
    ILogger<ProjectsController> logger) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all projects", OperationId = "GetAllProjects")]
    [SwaggerResponse(StatusCodes.Status200OK, "Projects found.", typeof(IEnumerable<ProjectResource>))]
    public async Task<IActionResult> GetAllProjects(CancellationToken cancellationToken)
    {
        var projects = await projectQueryService.Handle(new GetAllProjectsQuery(), cancellationToken);
        return Ok(ProjectResourceFromEntityAssembler.ToResourcesFromEntities(projects));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get a project by id", OperationId = "GetProjectById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project found.", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Project not found.")]
    public async Task<IActionResult> GetProjectById(int id, CancellationToken cancellationToken)
    {
        var project = await projectQueryService.Handle(new GetProjectByIdQuery(id), cancellationToken);
        if (project is null)
        {
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                ProjectsError.ProjectNotFound,
                localizer["ProjectsError.ProjectNotFound"]);
        }

        return Ok(ProjectResourceFromEntityAssembler.ToResourceFromEntity(project));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a project", OperationId = "CreateProject")]
    [SwaggerResponse(StatusCodes.Status201Created, "Project created.", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid project data.")]
    public async Task<IActionResult> CreateProject(
        [FromBody] CreateProjectResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateProjectCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await projectCommandService.CreateProject(command, cancellationToken);

        if (result.IsSuccess)
        {
            return CreatedAtAction(
                nameof(GetProjectById),
                new { id = result.Value!.Id },
                ProjectResourceFromEntityAssembler.ToResourceFromEntity(result.Value));
        }

        return MapErrorToActionResult(result);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update a project", OperationId = "UpdateProject")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project updated.", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid project data.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Project not found.")]
    public async Task<IActionResult> UpdateProject(
        int id,
        [FromBody] UpdateProjectResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateProjectCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await projectCommandService.Handle(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(ProjectResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));

        return MapErrorToActionResult(result);
    }

    private IActionResult MapErrorToActionResult(Result<Project> result)
    {
        var error = result.Error as ProjectsError?;
        var statusCode = error switch
        {
            ProjectsError.ProjectNotFound => StatusCodes.Status404NotFound,
            ProjectsError.InvalidProjectData => StatusCodes.Status400BadRequest,
            ProjectsError.OperationCancelled => StatusCodes.Status400BadRequest,
            ProjectsError.DatabaseError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            logger.LogError("Project operation failed: {Message}", result.Message);

        return problemDetailsFactory.CreateProblemDetails(this, statusCode, result.Error, result.Message);
    }
}
