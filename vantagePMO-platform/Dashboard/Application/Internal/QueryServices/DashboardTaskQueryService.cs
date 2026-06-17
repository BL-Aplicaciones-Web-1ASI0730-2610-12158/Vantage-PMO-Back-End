using vantagePMO_platform.Dashboard.Domain.Model.Queries;
using vantagePMO_platform.Dashboard.Domain.Repositories;
using vantagePMO_platform.Dashboard.Domain.Services;

namespace vantagePMO_platform.Dashboard.Application.Internal.QueryServices;

public class DashboardTaskQueryService(IDashboardTaskRepository dashboardTaskRepository) : IDashboardTaskQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.DashboardTask>> Handle(
        GetAllDashboardTasksQuery query,
        CancellationToken cancellationToken = default)
    {
        return await dashboardTaskRepository.ListOrderedAsync(cancellationToken);
    }
}
