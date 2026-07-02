namespace vantagePMO_platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     User object returned by <c>GET /users?username=&amp;password=</c> for front-end sign-in.
/// </summary>
public record FrontSignInUserResource(int Id, string Username, string? Email);
