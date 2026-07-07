using vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.RiskCompliance.Domain.Repositories;

public interface IRiskItemRepository : IBaseRepository<RiskItem>
{
    Task<IReadOnlyList<RiskItem>> ListOrderedAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

public interface IRiskMatrixRepository : IBaseRepository<RiskMatrix>
{
    Task<IReadOnlyList<RiskMatrix>> ListOrderedAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

public interface IComplianceMetricsRepository : IBaseRepository<ComplianceMetrics>
{
    Task<IReadOnlyList<ComplianceMetrics>> ListOrderedAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
