using System.Net.Mime;
using vantagePMO_platform.Iam.Application.CommandServices;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Iam.Interfaces.Rest.Resources;
using vantagePMO_platform.Iam.Interfaces.Rest.Transform;
using vantagePMO_platform.Iam.Resources;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Resources.Errors;
using vantagePMO_platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
// For IamError enum
// Corrected using directive
// For ProblemDetailsFactory

namespace vantagePMO_platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(
    IUserCommandService userCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer, // Renamed for clarity
    IStringLocalizer<IamMessages> iamLocalizer, // Inject IamMessages localizer
    ProblemDetailsFactory problemDetailsFactory) // Inject ProblemDetailsFactory
    : ControllerBase
{
    /**
     * <summary>
     *     Sign in endpoint. It allows authenticating a user
     * </summary>
     * <param name="signInResource">The sign-in resource containing username and password.</param>
     * <param name="cancellationToken">The cancellation token.</param>
     * <returns>The authenticated user resource, including a JWT token</returns>
     */
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in a user",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid username or password")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource,
        CancellationToken cancellationToken)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var result = await userCommandService.Handle(signInCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignInResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            userAndToken =>
                Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(userAndToken.user,
                    userAndToken.token))
        );
    }

    /**
     * <summary>
     *     Sign up endpoint. It creates a new user account and profile.
     * </summary>
     * <param name="signUpResource">The sign-up resource containing credentials and profile data.</param>
     * <param name="cancellationToken">The cancellation token.</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign-up",
        Description = "Sign up a new user and create their profile",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user and profile were created successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The sign-up data is invalid")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The username or email is already taken")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource,
        CancellationToken cancellationToken)
    {
        var commandResult = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource, errorLocalizer);
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

        return IamActionResultAssembler.ToActionResultFromSignUpResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            userId => Ok(new
            {
                id = userId,
                message = iamLocalizer["UserCreatedSuccessfully"].Value
            }));
    }
}