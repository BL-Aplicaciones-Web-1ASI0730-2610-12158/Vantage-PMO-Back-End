using vantagePMO_platform.Analytics.Domain.Model.Aggregates;
using vantagePMO_platform.Analytics.Domain.Model.Queries;

namespace vantagePMO_platform.Analytics.Application.QueryServices;

public interface IAnalyticsDashboardQueryService
{
    Task<IReadOnlyList<AnalyticsDashboard>> Handle(
        GetAllAnalyticsDashboardsQuery query,
        CancellationToken cancellationToken = default);
}
