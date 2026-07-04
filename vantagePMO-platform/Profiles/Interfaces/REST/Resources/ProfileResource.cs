namespace vantagePMO_platform.Profiles.Interfaces.Rest.Resources;

/// <summary>
///     Output resource (DTO) representing a profile exposed by the REST API.
/// </summary>
public record ProfileResource(
    int Id,
    string Name,
    string? DateOfBirth,
    string Role,
    string Email,
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
