using vantagePMO_platform.ResourcePlanning.Domain.Model.Aggregates;

namespace vantagePMO_platform.ResourcePlanning.Domain.Repositories;

public interface IResourcePlanningDashboardRepository
{
    Task<IReadOnlyList<ResourcePlanningDashboard>> ListOrderedAsync(CancellationToken cancellationToken = default);
    Task AddAsync(ResourcePlanningDashboard dashboard, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
