using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.ValueObjects;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace vantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     EF Core implementation of <see cref="IProfileRepository" />.
/// </summary>
public class ProfileRepository(AppDbContext context)
    : BaseRepository<Profile>(context), IProfileRepository
{
    /// <inheritdoc />
    public async Task<Profile?> FindByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Profile>()
            .FirstOrDefaultAsync(profile => profile.Email == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByEmailAsync(EmailAddress email, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Profile>()
            .AnyAsync(profile => profile.Email == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Profile?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Profile>()
            .FirstOrDefaultAsync(profile => profile.UserId == userId, cancellationToken);
    }
}
