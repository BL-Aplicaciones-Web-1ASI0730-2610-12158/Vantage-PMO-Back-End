using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProfileStatsRepository(AppDbContext context) : IProfileStatsRepository
{
    public async Task<ProfileStats?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await context.Set<ProfileStats>()
            .FirstOrDefaultAsync(stats => stats.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(ProfileStats stats, CancellationToken cancellationToken = default)
    {
        await context.Set<ProfileStats>().AddAsync(stats, cancellationToken);
    }
}
