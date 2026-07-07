using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;
using vantagePMO_platform.RiskCompliance.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace vantagePMO_platform.RiskCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RiskItemRepository(AppDbContext context)
    : BaseRepository<RiskItem>(context), IRiskItemRepository
{
    public async Task<IReadOnlyList<RiskItem>> ListOrderedAsync(CancellationToken cancellationToken = default) =>
        await context.Set<RiskItem>()
            .OrderBy(item => item.Id)
            .ToListAsync(cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<RiskItem>().AnyAsync(cancellationToken);
}

public class RiskMatrixRepository(AppDbContext context)
    : BaseRepository<RiskMatrix>(context), IRiskMatrixRepository
{
    public async Task<IReadOnlyList<RiskMatrix>> ListOrderedAsync(CancellationToken cancellationToken = default) =>
        await context.Set<RiskMatrix>()
            .OrderBy(matrix => matrix.Id)
            .ToListAsync(cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<RiskMatrix>().AnyAsync(cancellationToken);
}

public class ComplianceMetricsRepository(AppDbContext context)
    : BaseRepository<ComplianceMetrics>(context), IComplianceMetricsRepository
{
    public async Task<IReadOnlyList<ComplianceMetrics>> ListOrderedAsync(CancellationToken cancellationToken = default) =>
        await context.Set<ComplianceMetrics>()
            .OrderBy(metrics => metrics.Id)
            .ToListAsync(cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<ComplianceMetrics>().AnyAsync(cancellationToken);
}
