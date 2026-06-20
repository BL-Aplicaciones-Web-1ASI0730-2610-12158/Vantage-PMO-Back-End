using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Dashboard.Domain.Model.Queries;
using vantagePMO_platform.Dashboard.Application.QueryServices;
using vantagePMO_platform.Dashboard.Interfaces.Rest.Resources;
using vantagePMO_platform.Dashboard.Interfaces.Rest.Transform;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace vantagePMO_platform.Dashboard.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/tasks")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Home dashboard priority tasks")]
public class TasksController(IDashboardTaskQueryService dashboardTaskQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all dashboard tasks", OperationId = "GetAllDashboardTasks")]
    [SwaggerResponse(StatusCodes.Status200OK, "Tasks found.", typeof(IEnumerable<DashboardTaskResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await dashboardTaskQueryService.Handle(new GetAllDashboardTasksQuery(), cancellationToken);
        return Ok(DashboardTaskResourceFromEntityAssembler.ToResourcesFromEntities(tasks));
    }
}
