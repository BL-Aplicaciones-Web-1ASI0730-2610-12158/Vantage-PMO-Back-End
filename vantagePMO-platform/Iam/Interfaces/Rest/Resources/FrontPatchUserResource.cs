namespace vantagePMO_platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Front-end PATCH /users/{id} body (password recovery and optional profile fields).
/// </summary>
public record FrontPatchUserResource(
    string? Password,
    string? Name,
    string? Email,
    string? Role,
    string? DateOfBirth,
    string? Department,
    string? Joined,
    string? AvatarSeed,
    string? Availability,
    string? Experience,
    string? DeliveryRate,
    string? ActiveBudget,
    IEnumerable<string>? Bio,
    IEnumerable<string>? Certifications,
    string? SkillsDescription,
    string? Location,
    string? YearsActive,
    string? AvailabilityLabel);
