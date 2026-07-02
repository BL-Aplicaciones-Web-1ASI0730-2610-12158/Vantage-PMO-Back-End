using System.Text.Json.Serialization;

namespace vantagePMO_platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Sign-up payload used by the Vue front-end (<c>POST /users</c>).
/// </summary>
public record FrontSignUpResource(
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("birthDate")] string BirthDate);
