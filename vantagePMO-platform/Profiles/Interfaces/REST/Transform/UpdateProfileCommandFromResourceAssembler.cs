using VantagePMO_platform.Profiles.Domain.Model.Commands;
using VantagePMO_platform.Profiles.Interfaces.REST.Resources;

namespace VantagePMO_platform.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Maps an <see cref="UpdateProfileResource" /> to an <see cref="UpdateProfileCommand" />.
/// </summary>
public static class UpdateProfileCommandFromResourceAssembler
{
    /// <summary>Builds an update command for the given id from the supplied input resource.</summary>
    public static UpdateProfileCommand ToCommandFromResource(int profileId, UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(
            profileId,
            resource.Name,
            resource.Email,
            resource.Role,
            resource.Department,
            resource.Joined,
            resource.AvatarSeed,
            resource.Availability,
            resource.Experience,
            resource.DeliveryRate,
            resource.ActiveBudget,
            resource.Bio,
            resource.Certifications,
            resource.SkillsDescription,
            resource.Location,
            resource.YearsActive,
            resource.AvailabilityLabel);
    }
}
