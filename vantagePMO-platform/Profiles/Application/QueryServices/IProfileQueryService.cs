using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Queries;

namespace vantagePMO_platform.Profiles.Application.QueryServices;

/// <summary>
///     Query side of the Profiles bounded context (read operations).
/// </summary>
public interface IProfileQueryService
{
    /// <summary>
    ///     Handles retrieval of a profile by its identifier.
    /// </summary>
    /// <param name="query">The query carrying the profile id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The matching profile, or <c>null</c> when not found.</returns>
    Task<Profile?> Handle(GetProfileByIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles retrieval of a profile by its email address.
    /// </summary>
    /// <param name="query">The query carrying the email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The matching profile, or <c>null</c> when not found.</returns>
    Task<Profile?> Handle(GetProfileByEmailQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles retrieval of a profile by its linked IAM user identifier.
    /// </summary>
    /// <param name="query">The query carrying the user id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The matching profile, or <c>null</c> when not found.</returns>
    Task<Profile?> Handle(GetProfileByUserIdQuery query, CancellationToken cancellationToken = default);
}
