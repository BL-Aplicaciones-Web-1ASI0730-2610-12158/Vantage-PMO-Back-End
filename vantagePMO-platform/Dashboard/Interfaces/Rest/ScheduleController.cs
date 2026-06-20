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
[Route("api/v1/schedule")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Home dashboard schedule")]
public class ScheduleController(IScheduleItemQueryService scheduleItemQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all schedule items", OperationId = "GetAllScheduleItems")]
    [SwaggerResponse(StatusCodes.Status200OK, "Schedule items found.", typeof(IEnumerable<ScheduleItemResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await scheduleItemQueryService.Handle(new GetAllScheduleItemsQuery(), cancellationToken);
        return Ok(ScheduleItemResourceFromEntityAssembler.ToResourcesFromEntities(items));
    }
}
