using vantagePMO_platform.Reports.Domain.Model.Aggregates;
using vantagePMO_platform.Reports.Domain.Model.Queries;

namespace vantagePMO_platform.Reports.Application.QueryServices;

public interface IReportQueryService
{
    Task<IReadOnlyList<Report>> Handle(
        GetAllReportsQuery query,
        CancellationToken cancellationToken = default);
}
