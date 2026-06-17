using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Profiles.Domain.Model.Queries;
using vantagePMO_platform.Profiles.Domain.Services;
using vantagePMO_platform.Profiles.Interfaces.REST.Transform;

namespace vantagePMO_platform.Profiles.Interfaces.REST;

[AllowAnonymous]
[ApiController]
[Route("api/v1/stats")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Profile portfolio statistics")]
public class StatsController(IProfileStatsQueryService profileStatsQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get profile stats by user id", OperationId = "GetProfileStatsByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Stats found.")]
    public async Task<IActionResult> GetByUserId([FromQuery] int? userId, CancellationToken cancellationToken)
    {
        if (userId is null or <= 0)
            return Ok(Array.Empty<object>());

        var stats = await profileStatsQueryService.Handle(new GetProfileStatsByUserIdQuery(userId.Value), cancellationToken);
        if (stats is null)
            return Ok(Array.Empty<object>());

        return Ok(new[] { ProfileStatsResourceFromEntityAssembler.ToResourceFromEntity(stats) });
    }
}
