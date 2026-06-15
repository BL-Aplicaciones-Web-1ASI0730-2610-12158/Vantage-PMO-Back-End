namespace vantagePMO_platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Input resource used to register a new user and their profile.
/// </summary>
public record SignUpResource(
    string FullName,
    string Username,
    string Role,
    string DateOfBirth,
    string Email,
    string Password,
    string ConfirmPassword);
