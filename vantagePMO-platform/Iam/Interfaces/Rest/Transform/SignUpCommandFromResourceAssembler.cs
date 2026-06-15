using System.Globalization;
using Microsoft.Extensions.Localization;
using vantagePMO_platform.Iam.Domain.Model;
using vantagePMO_platform.Iam.Domain.Model.Commands;
using vantagePMO_platform.Iam.Interfaces.Rest.Resources;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Resources.Errors;

namespace vantagePMO_platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="SignUpResource" /> into a <see cref="SignUpCommand" />.
/// </summary>
public static class SignUpCommandFromResourceAssembler
{
    private const string DateOfBirthFormat = "dd/MM/yyyy";

    /// <summary>
    ///     Converts a <see cref="SignUpResource" /> to a <see cref="SignUpCommand" />.
    /// </summary>
    public static Result<SignUpCommand> ToCommandFromResource(
        SignUpResource resource,
        IStringLocalizer<ErrorMessages> localizer)
    {
        ArgumentNullException.ThrowIfNull(resource);

        if (!string.Equals(resource.Password, resource.ConfirmPassword, StringComparison.Ordinal))
        {
            return Result<SignUpCommand>.Failure(
                IamError.PasswordMismatch,
                localizer[$"IamError.{nameof(IamError.PasswordMismatch)}"]);
        }

        if (!DateOnly.TryParseExact(
                resource.DateOfBirth,
                DateOfBirthFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateOfBirth))
        {
            return Result<SignUpCommand>.Failure(
                IamError.InvalidDateOfBirth,
                localizer[$"IamError.{nameof(IamError.InvalidDateOfBirth)}", DateOfBirthFormat]);
        }

        return Result<SignUpCommand>.Success(new SignUpCommand(
            resource.Username.Trim(),
            resource.Password,
            resource.FullName.Trim(),
            resource.Email.Trim(),
            resource.Role.Trim(),
            dateOfBirth));
    }
}
