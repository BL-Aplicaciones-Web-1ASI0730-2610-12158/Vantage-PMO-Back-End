using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

namespace vantagePMO_platform.SystemAdministration.Domain.Repositories;

public interface ILoginAttemptRepository
{
    Task<IReadOnlyList<LoginAttempt>> ListOrderedAsync(CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<LoginAttempt> attempts, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
