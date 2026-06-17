using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Domain.Model.Queries;

namespace vantagePMO_platform.Dashboard.Domain.Services;

public interface IDashboardTaskQueryService
{
    Task<IReadOnlyList<DashboardTask>> Handle(
        GetAllDashboardTasksQuery query,
        CancellationToken cancellationToken = default);
}
