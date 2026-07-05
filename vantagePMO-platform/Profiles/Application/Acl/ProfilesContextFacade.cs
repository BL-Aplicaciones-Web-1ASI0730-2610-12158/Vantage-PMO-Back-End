using vantagePMO_platform.Profiles.Domain.Model.Commands;
using vantagePMO_platform.Profiles.Application.CommandServices;
using vantagePMO_platform.Profiles.Interfaces.Acl;
using vantagePMO_platform.Shared.Application.Model;

namespace vantagePMO_platform.Profiles.Application.Acl;

public class ProfilesContextFacade(IProfileCommandService profileCommandService) : IProfilesContextFacade
{
    public async Task<Result<int>> CreateProfile(CreateProfileRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProfileCommand(
            request.UserId,
            request.Name,
            request.DateOfBirth,
            request.Email,
            request.Role,
            request.Department,
            request.Joined,
            request.AvatarSeed,
            request.Availability,
            request.Experience,
            request.DeliveryRate,
            request.ActiveBudget,
            request.Bio,
            request.Certifications,
            request.SkillsDescription,
            request.Location,
            request.YearsActive,
            request.AvailabilityLabel);

        var result = await profileCommandService.CreateProfile(command, cancellationToken);
        return result.IsSuccess
            ? Result<int>.Success(result.Value!.Id)
            : Result<int>.Failure(result.Error!, result.Message);
    }
}
