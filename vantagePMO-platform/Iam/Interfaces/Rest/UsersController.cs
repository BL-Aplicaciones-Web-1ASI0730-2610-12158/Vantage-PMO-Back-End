using System.Globalization;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Application.CommandServices;
using vantagePMO_platform.Iam.Application.QueryServices;
using vantagePMO_platform.Iam.Domain.Model;
using vantagePMO_platform.Iam.Domain.Model.Aggregates;
using vantagePMO_platform.Iam.Domain.Model.Commands;
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
    IUserCommandService userCommandService,
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
    ///     Front-end compatibility: sign-in via query, profile lookup by email, or list users.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign-in, find by email, or list users", OperationId = "GetUsersOrProfiles")]
    [SwaggerResponse(StatusCodes.Status200OK, "Resources were found.")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] string? email,
        [FromQuery] string? username,
        [FromQuery] string? password,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
        {
            return await SignInForFrontAsync(new SignInCommand(username.Trim(), password), email, cancellationToken);
        }

        if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
        {
            var profile = await profileQueryService.Handle(new GetProfileByEmailQuery(email.Trim()), cancellationToken);
            if (profile is null || profile.UserId <= 0)
                return Ok(Array.Empty<FrontSignInUserResource>());

            var user = await userQueryService.Handle(new GetUserByIdQuery(profile.UserId), cancellationToken);
            if (user is null)
                return Ok(Array.Empty<FrontSignInUserResource>());

            return await SignInForFrontAsync(new SignInCommand(user.Username, password), email.Trim(), cancellationToken);
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            var profile = await profileQueryService.Handle(new GetProfileByEmailQuery(email), cancellationToken);
            if (profile is null)
                return Ok(Array.Empty<ProfileResource>());

            return Ok(new[] { ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile, exposeUserId: true) });
        }

        var users = await userQueryService.Handle(new GetAllUsersQuery(), cancellationToken);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    /// <summary>
    ///     Front-end compatibility: register via <c>POST /users</c> (Vue sign-up form).
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Register user (front-end contract)", OperationId = "RegisterUserForFront")]
    [SwaggerResponse(StatusCodes.Status201Created, "User created.", typeof(FrontSignInUserResource))]
    public async Task<IActionResult> RegisterUserForFront(
        [FromBody] FrontSignUpResource resource,
        CancellationToken cancellationToken)
    {
        var commandResult = FrontSignUpCommandFromResourceAssembler.ToCommandFromResource(resource, errorLocalizer);
        if (commandResult.IsFailure)
        {
            return IamActionResultAssembler.ToActionResultFromSignUpResult(
                this,
                Result.Failure(commandResult.Error!, commandResult.Message),
                errorLocalizer,
                problemDetailsFactory,
                () => Ok());
        }

        var result = await userCommandService.Handle(commandResult.Value!, cancellationToken);
        if (result.IsFailure)
        {
            return IamActionResultAssembler.ToActionResultFromSignUpResult(
                this,
                result,
                errorLocalizer,
                problemDetailsFactory,
                _ => Ok());
        }

        return StatusCode(
            StatusCodes.Status201Created,
            new FrontSignInUserResource(result.Value, resource.Username.Trim(), resource.Email.Trim()));
    }

    /// <summary>
    ///     Partially updates the profile or password for the given user id.
    /// </summary>
    [HttpPatch("{id:int}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Update profile or password by user id", OperationId = "UpdateProfileByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was updated.", typeof(ProfileResource))]
    public async Task<IActionResult> UpdateProfileByUserId(
        int id,
        [FromBody] FrontPatchUserResource resource,
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

        if (!string.IsNullOrWhiteSpace(resource.Password))
        {
            var passwordResult = await userCommandService.UpdatePasswordAsync(
                new UpdatePasswordCommand(id, resource.Password),
                cancellationToken);

            if (passwordResult.IsFailure)
                return MapIamErrorToActionResult(passwordResult);

            return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile, exposeUserId: true));
        }

        var profileResource = new UpdateProfileResource(
            resource.Name,
            resource.Email,
            resource.Role,
            resource.DateOfBirth,
            resource.Department,
            resource.Joined,
            resource.AvatarSeed,
            resource.Availability,
            resource.Experience,
            resource.DeliveryRate,
            resource.ActiveBudget,
            resource.Bio,
            resource.Certifications,
            resource.SkillsDescription,
            resource.Location,
            resource.YearsActive,
            resource.AvailabilityLabel);

        UpdateProfileCommand command;
        try
        {
            command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(profile.Id, profileResource);
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

    private async Task<IActionResult> SignInForFrontAsync(
        SignInCommand command,
        string? email,
        CancellationToken cancellationToken)
    {
        var result = await userCommandService.Handle(command, cancellationToken);
        if (result.IsFailure)
            return Ok(Array.Empty<FrontSignInUserResource>());

        var resolvedEmail = email;
        if (string.IsNullOrWhiteSpace(resolvedEmail))
        {
            var profile = await profileQueryService.Handle(
                new GetProfileByUserIdQuery(result.Value!.user.Id),
                cancellationToken);
            resolvedEmail = profile?.Email.Value;
        }

        return Ok(new[]
        {
            new FrontSignInUserResource(result.Value!.user.Id, result.Value.user.Username, resolvedEmail)
        });
    }

    private IActionResult MapIamErrorToActionResult(Result<User> result)
    {
        var error = result.Error as IamError?;
        var statusCode = error switch
        {
            IamError.UserNotFound => StatusCodes.Status404NotFound,
            IamError.InvalidProfileData => StatusCodes.Status400BadRequest,
            IamError.OperationCancelled => StatusCodes.Status400BadRequest,
            IamError.DatabaseError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };

        return problemDetailsFactory.CreateProblemDetails(this, statusCode, result.Error, result.Message);
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
