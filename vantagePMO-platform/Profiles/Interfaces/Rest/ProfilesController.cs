using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Profiles.Domain.Model;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Queries;
using vantagePMO_platform.Profiles.Application.CommandServices;
using vantagePMO_platform.Profiles.Application.QueryServices;
using vantagePMO_platform.Profiles.Interfaces.Rest.Resources;
using vantagePMO_platform.Profiles.Interfaces.Rest.Transform;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Interfaces.Rest.ProblemDetails;
using vantagePMO_platform.Shared.Resources.Errors;

namespace vantagePMO_platform.Profiles.Interfaces.Rest;

/// <summary>
///     REST endpoints for the Profiles bounded context.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Read and update profiles")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService,
    ProblemDetailsFactory problemDetailsFactory,
    IStringLocalizer<ErrorMessages> localizer,
    ILogger<ProfilesController> logger) : ControllerBase
{
    /// <summary>Retrieves a profile by its identifier.</summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get a profile by id",
        Description = "Returns the profile matching the supplied identifier.",
        OperationId = "GetProfileById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was found.", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The profile does not exist.")]
    public async Task<IActionResult> GetProfileById(int id, CancellationToken cancellationToken)
    {
        var profile = await profileQueryService.Handle(new GetProfileByIdQuery(id), cancellationToken);
        if (profile is null)
        {
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                ProfilesError.ProfileNotFound,
                localizer["ProfilesError.ProfileNotFound"]);
        }

        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    /// <summary>Retrieves a profile by its email address.</summary>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get a profile by email",
        Description = "Returns the profile matching the supplied email query parameter.",
        OperationId = "GetProfileByEmail")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was found.", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The email query parameter is missing.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The profile does not exist.")]
    public async Task<IActionResult> GetProfileByEmail(
        [FromQuery] string? email,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status400BadRequest,
                ProfilesError.InvalidProfileData,
                localizer["ProfilesError.InvalidProfileData"]);
        }

        var profile = await profileQueryService.Handle(new GetProfileByEmailQuery(email), cancellationToken);
        if (profile is null)
        {
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                ProfilesError.ProfileNotFound,
                localizer["ProfilesError.ProfileNotFound"]);
        }

        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    /// <summary>Partially updates an existing profile.</summary>
    [HttpPatch("{id:int}")]
    [SwaggerOperation(
        Summary = "Update a profile",
        Description = "Applies a partial update to the profile matching the supplied identifier.",
        OperationId = "UpdateProfile")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was updated.", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The profile data is invalid.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The profile does not exist.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The email is already registered.")]
    public async Task<IActionResult> UpdateProfile(
        int id,
        [FromBody] UpdateProfileResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await profileCommandService.Handle(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));

        return MapErrorToActionResult(result);
    }

    private IActionResult MapErrorToActionResult(Result<Profile> result)
    {
        var error = result.Error as ProfilesError?;
        var statusCode = error switch
        {
            ProfilesError.EmailAlreadyRegistered => StatusCodes.Status409Conflict,
            ProfilesError.ProfileNotFound => StatusCodes.Status404NotFound,
            ProfilesError.InvalidProfileData => StatusCodes.Status400BadRequest,
            ProfilesError.OperationCancelled => StatusCodes.Status400BadRequest,
            ProfilesError.DatabaseError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            logger.LogError("Profile operation failed: {Message}", result.Message);

        return problemDetailsFactory.CreateProblemDetails(this, statusCode, result.Error, result.Message);
    }
}
