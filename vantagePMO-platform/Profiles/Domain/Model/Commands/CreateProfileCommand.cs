namespace vantagePMO_platform.Profiles.Domain.Model.Commands;

/// <summary>
///     Command carrying all data required to create a new profile.
/// </summary>
public record CreateProfileCommand(
    string Name,
    DateOnly? DateOfBirth,
    string Email,
    string Role,
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
