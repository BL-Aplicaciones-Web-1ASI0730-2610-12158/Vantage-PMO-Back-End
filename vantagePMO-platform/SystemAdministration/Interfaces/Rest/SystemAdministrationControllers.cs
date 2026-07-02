using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.SystemAdministration.Application.CommandServices;
using vantagePMO_platform.SystemAdministration.Application.QueryServices;
using vantagePMO_platform.SystemAdministration.Interfaces.Rest.Resources;
using vantagePMO_platform.SystemAdministration.Interfaces.Rest.Transform;

namespace vantagePMO_platform.SystemAdministration.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/branding")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Platform branding")]
public class BrandingController(
    ISystemAdministrationCommandService commandService,
    ISystemAdministrationQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get branding settings", OperationId = "GetBranding")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var branding = await queryService.GetBrandingAsync(cancellationToken);
        return branding is null
            ? NotFound("Branding settings were not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(branding));
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update branding settings", OperationId = "UpdateBranding")]
    public async Task<IActionResult> Update(
        [FromBody] UpdateBrandingResource resource,
        CancellationToken cancellationToken)
    {
        var command = SystemAdministrationCommandFromResourceAssembler.ToCommand(resource);
        var branding = await commandService.UpdateBrandingAsync(command, cancellationToken);
        return branding is null
            ? NotFound("Branding settings were not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(branding));
    }
}

[AllowAnonymous]
[ApiController]
[Route("api/v1/subscription")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Platform subscription")]
public class SubscriptionController(
    ISystemAdministrationCommandService commandService,
    ISystemAdministrationQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get subscription", OperationId = "GetSubscription")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var subscription = await queryService.GetSubscriptionAsync(cancellationToken);
        return subscription is null
            ? NotFound("Subscription was not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(subscription));
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update subscription", OperationId = "UpdateSubscription")]
    public async Task<IActionResult> Update(
        [FromBody] UpdateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        var command = SystemAdministrationCommandFromResourceAssembler.ToCommand(resource);
        var subscription = await commandService.UpdateSubscriptionAsync(command, cancellationToken);
        return subscription is null
            ? NotFound("Subscription was not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(subscription));
    }

    [HttpGet("{id:int}/renew")]
    [SwaggerOperation(Summary = "Renew subscription", OperationId = "RenewSubscription")]
    public async Task<IActionResult> Renew(int id, CancellationToken cancellationToken)
    {
        var subscription = await commandService.RenewSubscriptionAsync(id, cancellationToken);
        return subscription is null
            ? NotFound($"Subscription {id} was not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(subscription));
    }
}

[AllowAnonymous]
[ApiController]
[Route("api/v1/admin-policy")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Global admin policies")]
public class AdminPolicyController(
    ISystemAdministrationCommandService commandService,
    ISystemAdministrationQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get admin policy", OperationId = "GetAdminPolicy")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var policy = await queryService.GetAdminPolicyAsync(cancellationToken);
        return policy is null
            ? NotFound("Admin policy was not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(policy));
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update admin policy", OperationId = "UpdateAdminPolicy")]
    public async Task<IActionResult> Update(
        [FromBody] UpdateAdminPolicyResource resource,
        CancellationToken cancellationToken)
    {
        var current = await queryService.GetAdminPolicyAsync(cancellationToken);
        if (current is null)
            return NotFound("Admin policy was not found.");

        var command = SystemAdministrationCommandFromResourceAssembler.ToCommand(resource, current);
        var policy = await commandService.UpdateAdminPolicyAsync(command, cancellationToken);
        return policy is null
            ? NotFound("Admin policy was not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(policy));
    }
}

[AllowAnonymous]
[ApiController]
[Route("api/v1/system-settings")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Global system settings")]
public class SystemSettingsController(
    ISystemAdministrationCommandService commandService,
    ISystemAdministrationQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get system settings", OperationId = "GetSystemSettings")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var settings = await queryService.GetSystemSettingsAsync(cancellationToken);
        return settings is null
            ? NotFound("System settings were not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(settings));
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update system settings", OperationId = "UpdateSystemSettings")]
    public async Task<IActionResult> Update(
        [FromBody] UpdateSystemSettingsResource resource,
        CancellationToken cancellationToken)
    {
        var current = await queryService.GetSystemSettingsAsync(cancellationToken);
        if (current is null)
            return NotFound("System settings were not found.");

        var command = SystemAdministrationCommandFromResourceAssembler.ToCommand(resource, current);
        var settings = await commandService.UpdateSystemSettingsAsync(command, cancellationToken);
        return settings is null
            ? NotFound("System settings were not found.")
            : Ok(SystemAdministrationResourceFromEntityAssembler.ToResource(settings));
    }
}

[AllowAnonymous]
[ApiController]
[Route("api/v1/login-attempts")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Recent login attempts")]
public class LoginAttemptsController(ISystemAdministrationQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get recent login attempts", OperationId = "GetLoginAttempts")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var attempts = await queryService.GetLoginAttemptsAsync(cancellationToken);
        return Ok(SystemAdministrationResourceFromEntityAssembler.ToResources(attempts));
    }
}
