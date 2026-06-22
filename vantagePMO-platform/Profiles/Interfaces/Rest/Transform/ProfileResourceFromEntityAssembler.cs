using System.Globalization;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Profiles.Interfaces.Rest.Transform;

public static class ProfileResourceFromEntityAssembler
{
    private const string DateOfBirthFormat = "dd/MM/yyyy";

    public static ProfileResource ToResourceFromEntity(Profile entity, bool exposeUserId = false)
    {
        return new ProfileResource(
            exposeUserId ? entity.UserId : entity.Id,
            entity.Name.FullName,
            entity.Role,
            entity.DateOfBirth?.ToString(DateOfBirthFormat, CultureInfo.InvariantCulture),
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
