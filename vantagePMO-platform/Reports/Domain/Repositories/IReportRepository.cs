using vantagePMO_platform.Reports.Domain.Model.Aggregates;

namespace vantagePMO_platform.Reports.Domain.Repositories;

public interface IReportRepository
{
    Task<IReadOnlyList<Report>> ListOrderedAsync(CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Report> reports, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
