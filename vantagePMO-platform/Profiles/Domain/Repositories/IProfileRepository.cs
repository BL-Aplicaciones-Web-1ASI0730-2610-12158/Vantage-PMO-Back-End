using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.ValueObjects;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Profiles.Domain.Repositories;

/// <summary>
///     Repository abstraction for the <see cref="Profile" /> aggregate.
/// </summary>
public interface IProfileRepository : IBaseRepository<Profile>
{
    /// <summary>
    ///     Finds a profile by its email address.
    /// </summary>
    /// <param name="email">The email address to look up.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The matching profile, or <c>null</c> when none exists.</returns>
    Task<Profile?> FindByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Determines whether a profile with the given email already exists.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> when a profile with the email exists; otherwise <c>false</c>.</returns>
    Task<bool> ExistsByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default);
}
