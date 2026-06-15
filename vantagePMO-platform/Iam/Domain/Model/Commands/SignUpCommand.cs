namespace vantagePMO_platform.Iam.Domain.Model.Commands;

/**
 * <summary>
 *     The sign up command
 * </summary>
 * <remarks>
 *     Carries the credentials and profile data collected during registration.
 * </remarks>
 */
public record SignUpCommand(
    string Username,
    string Password,
    string FullName,
    string Email,
    string Role,
    DateOnly DateOfBirth);
