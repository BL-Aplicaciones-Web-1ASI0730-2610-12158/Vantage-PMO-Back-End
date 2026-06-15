using System.Globalization;
using vantagePMO_platform.Profiles.Domain.Model.Commands;
using vantagePMO_platform.Profiles.Interfaces.REST.Resources;

namespace vantagePMO_platform.Profiles.Interfaces.REST.Transform;

public static class UpdateProfileCommandFromResourceAssembler
{
    private const string DateOfBirthFormat = "dd/MM/yyyy";

    public static UpdateProfileCommand ToCommandFromResource(int profileId, UpdateProfileResource resource)
    {
        DateOnly? dateOfBirth = null;
        if (resource.DateOfBirth is not null)
        {
            if (!DateOnly.TryParseExact(
                    resource.DateOfBirth,
                    DateOfBirthFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsed))
            {
                throw new ArgumentException($"Date of birth must use the format {DateOfBirthFormat}.");
            }

            dateOfBirth = parsed;
        }

        return new UpdateProfileCommand(
            profileId,
            resource.Name,
            resource.Email,
            resource.Role,
            dateOfBirth,
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
