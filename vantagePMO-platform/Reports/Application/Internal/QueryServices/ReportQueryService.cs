using vantagePMO_platform.Reports.Application.QueryServices;
using vantagePMO_platform.Reports.Domain.Model.Queries;
using vantagePMO_platform.Reports.Domain.Repositories;

namespace vantagePMO_platform.Reports.Application.Internal.QueryServices;

public class ReportQueryService(IReportRepository reportRepository) : IReportQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.Report>> Handle(
        GetAllReportsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await reportRepository.ListOrderedAsync(cancellationToken);
    }
}
