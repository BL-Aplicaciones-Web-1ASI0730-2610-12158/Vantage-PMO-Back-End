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
[SwaggerTag("Profile and portfolio statistics")]
public class StatsController(IProfileStatsQueryService profileStatsQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get stats by user id or list all portfolio stats",
        OperationId = "GetProfileStats")]
    [SwaggerResponse(StatusCodes.Status200OK, "Stats found.")]
    public async Task<IActionResult> GetStats([FromQuery] int? userId, CancellationToken cancellationToken)
    {
        if (userId is > 0)
        {
            var stats = await profileStatsQueryService.Handle(
                new GetProfileStatsByUserIdQuery(userId.Value),
                cancellationToken);

            if (stats is null)
                return Ok(Array.Empty<object>());

            return Ok(new[] { ProfileStatsResourceFromEntityAssembler.ToResourceFromEntity(stats) });
        }

        var allStats = await profileStatsQueryService.Handle(new GetAllProfileStatsQuery(), cancellationToken);
        return Ok(allStats.Select(ProfileStatsResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
