using vantagePMO_platform.Analytics.Application.QueryServices;
using vantagePMO_platform.Analytics.Domain.Model.Queries;
using vantagePMO_platform.Analytics.Domain.Repositories;

namespace vantagePMO_platform.Analytics.Application.Internal.QueryServices;

public class AnalyticsDashboardQueryService(IAnalyticsDashboardRepository analyticsDashboardRepository)
    : IAnalyticsDashboardQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.AnalyticsDashboard>> Handle(
        GetAllAnalyticsDashboardsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await analyticsDashboardRepository.ListOrderedAsync(cancellationToken);
    }
}
