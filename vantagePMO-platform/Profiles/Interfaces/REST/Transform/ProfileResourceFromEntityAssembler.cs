using VantagePMO_platform.Profiles.Domain.Model.Aggregates;
using VantagePMO_platform.Profiles.Interfaces.REST.Resources;

namespace VantagePMO_platform.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Maps a <see cref="Profile" /> aggregate to a <see cref="ProfileResource" />.
/// </summary>
public static class ProfileResourceFromEntityAssembler
{
    /// <summary>Builds an output resource from the supplied aggregate.</summary>
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(
            entity.Id,
            entity.Name.FullName,
            entity.Role,
            entity.Email.Value,
            entity.Department,
            entity.Joined,
            entity.AvatarSeed,
            entity.Availability,
            entity.Experience,
            entity.DeliveryRate,
            entity.ActiveBudget,
            entity.Bio,
            entity.Certifications,
            entity.SkillsDescription,
            entity.Location,
            entity.YearsActive,
            entity.AvailabilityLabel);
    }
}
