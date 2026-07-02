using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Settings.Application.CommandServices;
using vantagePMO_platform.Settings.Application.QueryServices;
using vantagePMO_platform.Settings.Domain.Model.Queries;
using vantagePMO_platform.Settings.Interfaces.Rest.Resources;
using vantagePMO_platform.Settings.Interfaces.Rest.Transform;

namespace vantagePMO_platform.Settings.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/user-settings")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("User settings (singleton)")]
public class UserSettingsController(
    IUserSettingsCommandService userSettingsCommandService,
    IUserSettingsQueryService userSettingsQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get user settings", OperationId = "GetUserSettings")]
    [SwaggerResponse(StatusCodes.Status200OK, "User settings found.", typeof(UserSettingsResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User settings not found.")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var settings = await userSettingsQueryService.Handle(new GetUserSettingsQuery(), cancellationToken);
        if (settings is null)
            return NotFound("User settings were not found.");

        return Ok(UserSettingsResourceFromEntityAssembler.ToResourceFromEntity(settings));
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update user settings", OperationId = "UpdateUserSettings")]
    [SwaggerResponse(StatusCodes.Status200OK, "User settings updated.", typeof(UserSettingsResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User settings not found.")]
    public async Task<IActionResult> Update(
        [FromBody] UpdateUserSettingsResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateUserSettingsCommandFromResourceAssembler.ToCommandFromResource(resource);
        var settings = await userSettingsCommandService.UpdateAsync(command, cancellationToken);

        if (settings is null)
            return NotFound("User settings were not found.");

        return Ok(UserSettingsResourceFromEntityAssembler.ToResourceFromEntity(settings));
    }
}
