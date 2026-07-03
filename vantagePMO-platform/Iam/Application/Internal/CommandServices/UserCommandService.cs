using System.Globalization;
using vantagePMO_platform.Iam.Application.CommandServices;
using vantagePMO_platform.Iam.Application.Internal.OutboundServices;
using vantagePMO_platform.Iam.Domain.Model;
using vantagePMO_platform.Iam.Domain.Model.Aggregates;
using vantagePMO_platform.Iam.Domain.Model.Commands;
using vantagePMO_platform.Iam.Domain.Repositories;
using vantagePMO_platform.Profiles.Domain.Model;
using vantagePMO_platform.Profiles.Application.Internal.CommandServices;
using vantagePMO_platform.Profiles.Interfaces.Acl;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace vantagePMO_platform.Iam.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IProfilesContextFacade profilesContextFacade,
    ProfileRelatedDataSeeder profileRelatedDataSeeder,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IUserCommandService
{
    public async Task<Result<(User user, string token)>> Handle(SignInCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username, cancellationToken);

        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(
                IamError.InvalidCredentials,
                localizer[$"IamError.{nameof(IamError.InvalidCredentials)}"]);

        var token = tokenService.GenerateToken(user);

        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByUsernameAsync(command.Username, cancellationToken))
            return Result.Failure(
                IamError.UsernameAlreadyTaken,
                localizer[$"IamError.{nameof(IamError.UsernameAlreadyTaken)}", command.Username]);

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            var firstName = command.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()
                            ?? command.FullName;
            var joined = DateTime.UtcNow.ToString("MMMM yyyy", CultureInfo.InvariantCulture);

            var profileResult = await profilesContextFacade.CreateProfile(
                new CreateProfileRequest(
                    user.Id,
                    command.FullName,
                    command.Email,
                    command.Role,
                    command.DateOfBirth,
                    string.Empty,
                    joined,
                    firstName,
                    "AVAILABLE FOR CONSULT",
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    new[] { $"Professional at Vantage PMO with a focus on {command.Role}." },
                    Array.Empty<string>(),
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "Accepting New Leads"),
                cancellationToken);

            if (profileResult.IsFailure)
            {
                userRepository.Remove(user);
                await unitOfWork.CompleteAsync(cancellationToken);
                return MapProfileErrorToSignUpResult(profileResult);
            }

            await profileRelatedDataSeeder.SeedDefaultsAsync(user.Id, command.FullName, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(
                IamError.OperationCancelled,
                localizer[$"IamError.{nameof(IamError.OperationCancelled)}"]);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(
                IamError.DatabaseError,
                localizer[$"IamError.{nameof(IamError.DatabaseError)}"]);
        }
        catch (Exception)
        {
            return Result.Failure(
                IamError.InternalServerError,
                localizer[$"IamError.{nameof(IamError.InternalServerError)}"]);
        }
    }

    public async Task<Result<User>> UpdatePasswordAsync(
        UpdatePasswordCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Password))
        {
            return Result<User>.Failure(
                IamError.InvalidProfileData,
                localizer[$"IamError.{nameof(IamError.InvalidProfileData)}"]);
        }

        var user = await userRepository.FindByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            return Result<User>.Failure(
                IamError.UserNotFound,
                localizer[$"IamError.{nameof(IamError.UserNotFound)}"]);
        }

        try
        {
            user.UpdatePasswordHash(hashingService.HashPassword(command.Password));
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<User>.Success(user);
        }
        catch (OperationCanceledException)
        {
            return Result<User>.Failure(
                IamError.OperationCancelled,
                localizer[$"IamError.{nameof(IamError.OperationCancelled)}"]);
        }
        catch (DbUpdateException)
        {
            return Result<User>.Failure(
                IamError.DatabaseError,
                localizer[$"IamError.{nameof(IamError.DatabaseError)}"]);
        }
        catch (Exception)
        {
            return Result<User>.Failure(
                IamError.InternalServerError,
                localizer[$"IamError.{nameof(IamError.InternalServerError)}"]);
        }
    }

    private Result MapProfileErrorToSignUpResult(Result<int> profileResult)
    {
        return profileResult.Error switch
        {
            ProfilesError.EmailAlreadyRegistered =>
                Result.Failure(IamError.EmailAlreadyRegistered, profileResult.Message),
            ProfilesError.InvalidProfileData =>
                Result.Failure(IamError.InvalidProfileData, profileResult.Message),
            ProfilesError.OperationCancelled =>
                Result.Failure(IamError.OperationCancelled, profileResult.Message),
            ProfilesError.DatabaseError =>
                Result.Failure(IamError.DatabaseError, profileResult.Message),
            _ =>
                Result.Failure(IamError.InternalServerError, profileResult.Message)
        };
    }
}
