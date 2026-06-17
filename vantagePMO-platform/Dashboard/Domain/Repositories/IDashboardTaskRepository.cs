using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;

namespace vantagePMO_platform.Dashboard.Domain.Repositories;

public interface IDashboardTaskRepository
{
    Task<IReadOnlyList<DashboardTask>> ListOrderedAsync(CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<DashboardTask> tasks, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
