using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Reports.Application.QueryServices;
using vantagePMO_platform.Reports.Domain.Model.Queries;
using vantagePMO_platform.Reports.Interfaces.Rest.Resources;
using vantagePMO_platform.Reports.Interfaces.Rest.Transform;

namespace vantagePMO_platform.Reports.Interfaces.Rest;

[ApiController]
[Route("api/v1/reports")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Quick Reports for project leaders (Segment 1)")]
public class ReportsController(IReportQueryService reportQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all executive project reports", OperationId = "GetAllReports")]
    [SwaggerResponse(StatusCodes.Status200OK, "Reports found.", typeof(IEnumerable<ReportResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var reports = await reportQueryService.Handle(new GetAllReportsQuery(), cancellationToken);
        return Ok(ReportResourceFromEntityAssembler.ToResourcesFromEntities(reports));
    }
}
