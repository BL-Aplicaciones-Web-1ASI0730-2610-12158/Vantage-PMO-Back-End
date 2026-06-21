using vantagePMO_platform.Analytics.Domain.Model.Aggregates;

namespace vantagePMO_platform.Analytics.Domain.Repositories;

public interface IAnalyticsDashboardRepository
{
    Task<IReadOnlyList<AnalyticsDashboard>> ListOrderedAsync(CancellationToken cancellationToken = default);
    Task AddAsync(AnalyticsDashboard dashboard, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
