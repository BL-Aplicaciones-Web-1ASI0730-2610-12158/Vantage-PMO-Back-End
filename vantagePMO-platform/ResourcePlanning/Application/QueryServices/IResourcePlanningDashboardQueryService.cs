using vantagePMO_platform.ResourcePlanning.Domain.Model.Aggregates;
using vantagePMO_platform.ResourcePlanning.Domain.Model.Queries;

namespace vantagePMO_platform.ResourcePlanning.Application.QueryServices;

public interface IResourcePlanningDashboardQueryService
{
    Task<IReadOnlyList<ResourcePlanningDashboard>> Handle(
        GetAllResourcePlanningDashboardsQuery query,
        CancellationToken cancellationToken = default);
}
