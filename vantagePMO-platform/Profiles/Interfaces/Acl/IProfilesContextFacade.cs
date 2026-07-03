using vantagePMO_platform.Shared.Application.Model;

namespace vantagePMO_platform.Profiles.Interfaces.Acl;

/// <summary>
///     Anti-corruption layer for cross-context profile operations.
/// </summary>
public interface IProfilesContextFacade
{
    /// <summary>
    ///     Creates a profile without committing the unit of work.
    ///     The caller is responsible for calling <see cref="Shared.Domain.Repositories.IUnitOfWork.CompleteAsync" />.
    /// </summary>
    Task<Result<int>> CreateProfile(CreateProfileRequest request, CancellationToken cancellationToken);
}
