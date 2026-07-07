using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.ResourcePlanning.Application.QueryServices;
using vantagePMO_platform.ResourcePlanning.Domain.Model.Queries;
using vantagePMO_platform.ResourcePlanning.Interfaces.Rest.Resources;
using vantagePMO_platform.ResourcePlanning.Interfaces.Rest.Transform;

namespace vantagePMO_platform.ResourcePlanning.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/resource-planning")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Resource planning dashboard")]
public class ResourcePlanningController(IResourcePlanningDashboardQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all resource planning dashboards", OperationId = "GetAllResourcePlanningDashboards")]
    [SwaggerResponse(StatusCodes.Status200OK, "Resource planning dashboards found.", typeof(IEnumerable<ResourcePlanningDashboardResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var dashboards = await queryService.Handle(
            new GetAllResourcePlanningDashboardsQuery(),
            cancellationToken);

        return Ok(ResourcePlanningDashboardResourceFromEntityAssembler.ToResourcesFromEntities(dashboards));
    }
}
