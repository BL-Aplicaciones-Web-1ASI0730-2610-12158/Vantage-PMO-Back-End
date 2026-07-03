using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Queries;
using vantagePMO_platform.Profiles.Domain.Model.ValueObjects;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Profiles.Application.QueryServices;

namespace vantagePMO_platform.Profiles.Application.Internal.QueryServices;

/// <summary>
///     Query service handling the read operations of the Profiles bounded context.
/// </summary>
public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByEmailQuery query, CancellationToken cancellationToken = default)
    {
        EmailAddress email;
        try
        {
            email = new EmailAddress(query.Email);
        }
        catch (ArgumentException)
        {
            return null;
        }

        return await profileRepository.FindByEmailAsync(email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByUserIdQuery query, CancellationToken cancellationToken = default)
    {
        return await profileRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}
