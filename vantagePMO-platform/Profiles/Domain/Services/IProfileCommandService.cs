using VantagePMO_platform.Profiles.Domain.Model.Aggregates;
using VantagePMO_platform.Profiles.Domain.Model.Commands;
using VantagePMO_platform.Shared.Application.Model;

namespace VantagePMO_platform.Profiles.Domain.Services;

/// <summary>
///     Command side of the Profiles bounded context (write operations).
/// </summary>
public interface IProfileCommandService
{
    /// <summary>
    ///     Handles the creation of a new profile.
    /// </summary>
    /// <param name="command">The creation command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Result{T}" /> wrapping the created profile or a business error.</returns>
    Task<Result<Profile>> Handle(CreateProfileCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles a partial update of an existing profile.
    /// </summary>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Result{T}" /> wrapping the updated profile or a business error.</returns>
    Task<Result<Profile>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken = default);
}
