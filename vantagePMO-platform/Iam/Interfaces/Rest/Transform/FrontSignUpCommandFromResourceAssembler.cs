using System.Globalization;
using Microsoft.Extensions.Localization;
using vantagePMO_platform.Iam.Domain.Model;
using vantagePMO_platform.Iam.Domain.Model.Commands;
using vantagePMO_platform.Iam.Interfaces.Rest.Resources;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Resources.Errors;

namespace vantagePMO_platform.Iam.Interfaces.Rest.Transform;

public static class FrontSignUpCommandFromResourceAssembler
{
    private static readonly string[] SupportedDateFormats = ["yyyy-MM-dd", "dd/MM/yyyy"];

    public static Result<SignUpCommand> ToCommandFromResource(
        FrontSignUpResource resource,
        IStringLocalizer<ErrorMessages> localizer)
    {
        ArgumentNullException.ThrowIfNull(resource);

        if (string.IsNullOrWhiteSpace(resource.Username)
            || string.IsNullOrWhiteSpace(resource.Password)
            || string.IsNullOrWhiteSpace(resource.Name)
            || string.IsNullOrWhiteSpace(resource.Email))
        {
            return Result<SignUpCommand>.Failure(
                IamError.InvalidProfileData,
                localizer[$"IamError.{nameof(IamError.InvalidProfileData)}"]);
        }

        if (!DateOnly.TryParseExact(
                resource.BirthDate?.Trim() ?? string.Empty,
                SupportedDateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateOfBirth))
        {
            return Result<SignUpCommand>.Failure(
                IamError.InvalidDateOfBirth,
                localizer[$"IamError.{nameof(IamError.InvalidDateOfBirth)}", "yyyy-MM-dd"]);
        }

        return Result<SignUpCommand>.Success(new SignUpCommand(
            resource.Username.Trim(),
            resource.Password,
            resource.Name.Trim(),
            resource.Email.Trim(),
            resource.Role?.Trim() ?? string.Empty,
            dateOfBirth));
    }
}
