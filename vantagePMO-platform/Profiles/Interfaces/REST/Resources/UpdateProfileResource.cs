namespace vantagePMO_platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Input resource (DTO) used to partially update a profile (PATCH semantics).
///     Omitted/<c>null</c> fields are left unchanged.
/// </summary>
public record UpdateProfileResource(
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
