using System.Globalization;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Application.QueryServices;
using vantagePMO_platform.Iam.Domain.Model.Queries;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Iam.Interfaces.Rest.Resources;
using vantagePMO_platform.Iam.Interfaces.Rest.Transform;
using vantagePMO_platform.Profiles.Domain.Model;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Commands;
using vantagePMO_platform.Profiles.Domain.Model.Queries;
using vantagePMO_platform.Profiles.Application.CommandServices;
using vantagePMO_platform.Profiles.Application.QueryServices;
using vantagePMO_platform.Profiles.Interfaces.Rest.Resources;
using vantagePMO_platform.Profiles.Interfaces.Rest.Transform;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Interfaces.Rest.ProblemDetails;
using vantagePMO_platform.Shared.Resources.Errors;

namespace vantagePMO_platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("User and profile endpoints")]
public class UsersController(
    IUserQueryService userQueryService,
    IProfileQueryService profileQueryService,
    IProfileCommandService profileCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory,
    ILogger<UsersController> logger) : ControllerBase
{
    /// <summary>
    ///     Returns the profile for the given user id. Matches the frontend <c>/users/{id}</c> contract.
    /// </summary>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Get profile by user id", OperationId = "GetProfileByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was found.", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The profile does not exist.")]
    public async Task<IActionResult> GetProfileByUserId(int id, CancellationToken cancellationToken)
    {
        var profile = await profileQueryService.Handle(new GetProfileByUserIdQuery(id), cancellationToken);
        if (profile is null)
        {
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                ProfilesError.ProfileNotFound,
                errorLocalizer["ProfilesError.ProfileNotFound"]);
        }

        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile, exposeUserId: true));
    }

    /// <summary>
    ///     Returns profiles filtered by email, or all IAM users when no email is supplied.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Get profiles by email or list IAM users", OperationId = "GetUsersOrProfiles")]
    [SwaggerResponse(StatusCodes.Status200OK, "Resources were found.")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] string? email,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            var profile = await profileQueryService.Handle(new GetProfileByEmailQuery(email), cancellationToken);
            if (profile is null)
                return Ok(Array.Empty<ProfileResource>());

            return Ok(new[] { ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile, exposeUserId: true) });
        }

        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery, cancellationToken);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    /// <summary>
    ///     Partially updates the profile linked to the given user id.
    /// </summary>
    [HttpPatch("{id:int}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Update profile by user id", OperationId = "UpdateProfileByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was updated.", typeof(ProfileResource))]
    public async Task<IActionResult> UpdateProfileByUserId(
        int id,
        [FromBody] UpdateProfileResource resource,
        CancellationToken cancellationToken)
    {
        var profile = await profileQueryService.Handle(new GetProfileByUserIdQuery(id), cancellationToken);
        if (profile is null)
        {
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status404NotFound,
                ProfilesError.ProfileNotFound,
                errorLocalizer["ProfilesError.ProfileNotFound"]);
        }

        UpdateProfileCommand command;
        try
        {
            command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(profile.Id, resource);
        }
        catch (ArgumentException)
        {
            return problemDetailsFactory.CreateProblemDetails(
                this,
                StatusCodes.Status400BadRequest,
                ProfilesError.InvalidProfileData,
                errorLocalizer["ProfilesError.InvalidProfileData"]);
        }

        var result = await profileCommandService.Handle(command, cancellationToken);
        if (result.IsSuccess)
            return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result.Value!, exposeUserId: true));

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
