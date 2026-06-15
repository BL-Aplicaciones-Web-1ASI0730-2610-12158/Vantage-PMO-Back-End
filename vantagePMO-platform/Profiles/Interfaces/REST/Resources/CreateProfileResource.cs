namespace VantagePMO_platform.Profiles.Interfaces.REST.Resources;

/// <summary>
///     Input resource (DTO) used to create a new profile.
/// </summary>
public record CreateProfileResource(
    string Name,
    string Email,
    string? Role,
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
