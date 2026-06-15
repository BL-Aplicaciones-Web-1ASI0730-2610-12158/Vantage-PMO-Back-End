using VantagePMO_platform.Profiles.Domain.Model.Commands;
using VantagePMO_platform.Profiles.Interfaces.REST.Resources;

namespace VantagePMO_platform.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Maps a <see cref="CreateProfileResource" /> to a <see cref="CreateProfileCommand" />.
/// </summary>
public static class CreateProfileCommandFromResourceAssembler
{
    /// <summary>Builds a creation command from the supplied input resource.</summary>
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.Name,
            resource.Email,
            resource.Role ?? string.Empty,
            resource.Department ?? string.Empty,
            resource.Joined ?? string.Empty,
            resource.AvatarSeed ?? string.Empty,
            resource.Availability ?? string.Empty,
            resource.Experience ?? string.Empty,
            resource.DeliveryRate ?? string.Empty,
            resource.ActiveBudget ?? string.Empty,
            resource.Bio ?? new List<string>(),
            resource.Certifications ?? new List<string>(),
            resource.SkillsDescription ?? string.Empty,
            resource.Location ?? string.Empty,
            resource.YearsActive ?? string.Empty,
            resource.AvailabilityLabel ?? string.Empty);
    }
}
