namespace vantagePMO_platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Command carrying a partial set of profile changes (PATCH semantics).
/// </summary>
/// <remarks>
///     Every field is nullable. A <c>null</c> value means the corresponding field is
///     left untouched, allowing partial updates from the client.
/// </remarks>
public record UpdateProfileCommand(
    int ProfileId,
    string? Name,
    string? Email,
    string? Role,
    DateOnly? DateOfBirth,
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
