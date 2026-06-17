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
[Route("api/v1/profile-skills")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Profile skills")]
public class ProfileSkillsController(IProfileSkillQueryService profileSkillQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get profile skills by user id", OperationId = "GetProfileSkillsByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Skills found.")]
    public async Task<IActionResult> GetByUserId([FromQuery] int? userId, CancellationToken cancellationToken)
    {
        if (userId is null or <= 0)
            return Ok(Array.Empty<object>());

        var skills = await profileSkillQueryService.Handle(new GetProfileSkillsByUserIdQuery(userId.Value), cancellationToken);
        return Ok(ProfileSkillResourceFromEntityAssembler.ToResourcesFromEntities(skills));
    }
}
