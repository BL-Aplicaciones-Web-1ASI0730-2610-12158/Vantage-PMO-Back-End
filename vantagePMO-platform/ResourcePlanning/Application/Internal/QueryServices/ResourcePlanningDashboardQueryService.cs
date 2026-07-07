using vantagePMO_platform.ResourcePlanning.Application.QueryServices;
using vantagePMO_platform.ResourcePlanning.Domain.Model.Queries;
using vantagePMO_platform.ResourcePlanning.Domain.Repositories;

namespace vantagePMO_platform.ResourcePlanning.Application.Internal.QueryServices;

public class ResourcePlanningDashboardQueryService(IResourcePlanningDashboardRepository repository)
    : IResourcePlanningDashboardQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.ResourcePlanningDashboard>> Handle(
        GetAllResourcePlanningDashboardsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await repository.ListOrderedAsync(cancellationToken);
    }
}
