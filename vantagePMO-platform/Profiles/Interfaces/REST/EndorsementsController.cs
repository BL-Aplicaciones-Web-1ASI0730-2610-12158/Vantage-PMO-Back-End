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
[Route("api/v1/endorsements")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Profile endorsements")]
public class EndorsementsController(IEndorsementQueryService endorsementQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get endorsements by user id", OperationId = "GetEndorsementsByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Endorsements found.")]
    public async Task<IActionResult> GetByUserId([FromQuery] int? userId, CancellationToken cancellationToken)
    {
        if (userId is null or <= 0)
            return Ok(Array.Empty<object>());

        var endorsements = await endorsementQueryService.Handle(new GetEndorsementsByUserIdQuery(userId.Value), cancellationToken);
        return Ok(EndorsementResourceFromEntityAssembler.ToResourcesFromEntities(endorsements));
    }
}
