using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Analytics.Application.QueryServices;
using vantagePMO_platform.Analytics.Domain.Model.Queries;
using vantagePMO_platform.Analytics.Interfaces.Rest.Resources;
using vantagePMO_platform.Analytics.Interfaces.Rest.Transform;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace vantagePMO_platform.Analytics.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/analytics")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Portfolio analytics dashboard")]
public class AnalyticsController(IAnalyticsDashboardQueryService analyticsDashboardQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all analytics dashboards", OperationId = "GetAllAnalyticsDashboards")]
    [SwaggerResponse(StatusCodes.Status200OK, "Analytics dashboards found.", typeof(IEnumerable<AnalyticsDashboardResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var dashboards = await analyticsDashboardQueryService.Handle(
            new GetAllAnalyticsDashboardsQuery(),
            cancellationToken);

        return Ok(AnalyticsDashboardResourceFromEntityAssembler.ToResourcesFromEntities(dashboards));
    }
}
