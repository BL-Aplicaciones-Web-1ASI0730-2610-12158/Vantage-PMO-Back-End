using vantagePMO_platform.Profiles.Domain.Model.Aggregates;

namespace vantagePMO_platform.Profiles.Domain.Repositories;

public interface IProfileStatsRepository
{
    Task<ProfileStats?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProfileStats>> ListOrderedAsync(CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    Task AddAsync(ProfileStats stats, CancellationToken cancellationToken = default);
}
