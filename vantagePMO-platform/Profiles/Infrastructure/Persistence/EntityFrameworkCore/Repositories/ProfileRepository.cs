using Microsoft.EntityFrameworkCore;
using VantagePMO_platform.Profiles.Domain.Model.Aggregates;
using VantagePMO_platform.Profiles.Domain.Model.ValueObjects;
using VantagePMO_platform.Profiles.Domain.Repositories;
using VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace VantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

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
}
