namespace vantagePMO_platform.Profiles.Interfaces.Acl;

/// <summary>
///     Cross-context request used to create a profile from another bounded context.
/// </summary>
public record CreateProfileRequest(
    string Name,
    string Email,
    string Role,
    DateOnly? DateOfBirth,
    string Department,
    string Joined,
    string AvatarSeed,
    string Availability,
    string Experience,
    string DeliveryRate,
    string ActiveBudget,
    IEnumerable<string> Bio,
    IEnumerable<string> Certifications,
    string SkillsDescription,
    string Location,
    string YearsActive,
    string AvailabilityLabel);
