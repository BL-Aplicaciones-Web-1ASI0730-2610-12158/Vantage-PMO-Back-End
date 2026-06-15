using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using vantagePMO_platform.Profiles.Application.Errors;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Commands;
using vantagePMO_platform.Profiles.Domain.Model.ValueObjects;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Profiles.Domain.Services;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.Shared.Resources.Errors;

namespace vantagePMO_platform.Profiles.Application.Internal.CommandServices;

/// <summary>
///     Command service handling the write operations of the Profiles bounded context.
/// </summary>
public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer,
    ILogger<ProfileCommandService> logger) : IProfileCommandService
{
    /// <inheritdoc />
    public async Task<Result<Profile>> CreateProfile(CreateProfileCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            Profile profile;
            try
            {
                profile = new Profile(command);
            }
            catch (ArgumentException exception)
            {
                logger.LogWarning(exception, "Invalid profile data while creating profile.");
                return Result<Profile>.Failure(
                    ProfilesError.InvalidProfileData,
                    localizer["ProfilesError.InvalidProfileData"]);
            }

            if (await profileRepository.ExistsByEmailAsync(profile.Email, cancellationToken))
            {
                return Result<Profile>.Failure(
                    ProfilesError.EmailAlreadyRegistered,
                    localizer["ProfilesError.EmailAlreadyRegistered", profile.Email.Value]);
            }

            await profileRepository.AddAsync(profile, cancellationToken);

            logger.LogInformation("Profile {ProfileId} staged for creation.", profile.Id);
            return Result<Profile>.Success(profile);
        }
        catch (OperationCanceledException)
        {
            return Result<Profile>.Failure(
                ProfilesError.OperationCancelled,
                localizer["ProfilesError.OperationCancelled"]);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Database error while creating profile.");
            return Result<Profile>.Failure(
                ProfilesError.DatabaseError,
                localizer["ProfilesError.DatabaseError"]);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error while creating profile.");
            return Result<Profile>.Failure(
                ProfilesError.InternalServerError,
                localizer["ProfilesError.InternalServerError"]);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Profile>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var profile = await profileRepository.FindByIdAsync(command.ProfileId, cancellationToken);
            if (profile is null)
            {
                return Result<Profile>.Failure(
                    ProfilesError.ProfileNotFound,
                    localizer["ProfilesError.ProfileNotFound"]);
            }

            if (command.Email is not null)
            {
                EmailAddress newEmail;
                try
                {
                    newEmail = new EmailAddress(command.Email);
                }
                catch (ArgumentException exception)
                {
                    logger.LogWarning(exception, "Invalid email while updating profile {ProfileId}.", command.ProfileId);
                    return Result<Profile>.Failure(
                        ProfilesError.InvalidProfileData,
                        localizer["ProfilesError.InvalidProfileData"]);
                }

                if (newEmail.Value != profile.Email.Value
                    && await profileRepository.ExistsByEmailAsync(newEmail, cancellationToken))
                {
                    return Result<Profile>.Failure(
                        ProfilesError.EmailAlreadyRegistered,
                        localizer["ProfilesError.EmailAlreadyRegistered", newEmail.Value]);
                }
            }

            try
            {
                profile.Update(command);
            }
            catch (ArgumentException exception)
            {
                logger.LogWarning(exception, "Invalid profile data while updating profile {ProfileId}.", command.ProfileId);
                return Result<Profile>.Failure(
                    ProfilesError.InvalidProfileData,
                    localizer["ProfilesError.InvalidProfileData"]);
            }

            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync(cancellationToken);

            logger.LogInformation("Profile {ProfileId} updated.", profile.Id);
            return Result<Profile>.Success(profile);
        }
        catch (OperationCanceledException)
        {
            return Result<Profile>.Failure(
                ProfilesError.OperationCancelled,
                localizer["ProfilesError.OperationCancelled"]);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Database error while updating profile {ProfileId}.", command.ProfileId);
            return Result<Profile>.Failure(
                ProfilesError.DatabaseError,
                localizer["ProfilesError.DatabaseError"]);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error while updating profile {ProfileId}.", command.ProfileId);
            return Result<Profile>.Failure(
                ProfilesError.InternalServerError,
                localizer["ProfilesError.InternalServerError"]);
        }
    }
}
