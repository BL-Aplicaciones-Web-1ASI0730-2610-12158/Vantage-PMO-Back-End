using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Reports.Domain.Model.Aggregates;
using vantagePMO_platform.Reports.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Reports.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ReportRepository(AppDbContext context) : IReportRepository
{
    public async Task<IReadOnlyList<Report>> ListOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Report>()
            .OrderBy(report => report.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Report> reports, CancellationToken cancellationToken = default)
    {
        await context.Set<Report>().AddRangeAsync(reports, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Report>().AnyAsync(cancellationToken);
    }
}
