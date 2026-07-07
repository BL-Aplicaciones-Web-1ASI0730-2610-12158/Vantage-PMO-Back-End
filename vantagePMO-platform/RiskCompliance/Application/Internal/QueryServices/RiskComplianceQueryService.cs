using vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;
using vantagePMO_platform.RiskCompliance.Domain.Model.Queries;
using vantagePMO_platform.RiskCompliance.Application.QueryServices;
using vantagePMO_platform.RiskCompliance.Domain.Repositories;

namespace vantagePMO_platform.RiskCompliance.Application.Internal.QueryServices;

public class RiskComplianceQueryService(
    IRiskItemRepository riskItemRepository,
    IRiskMatrixRepository riskMatrixRepository,
    IComplianceMetricsRepository complianceMetricsRepository) : IRiskComplianceQueryService
{
    public Task<IReadOnlyList<RiskItem>> Handle(
        GetAllRisksQuery query,
        CancellationToken cancellationToken = default) =>
        riskItemRepository.ListOrderedAsync(cancellationToken);

    public Task<IReadOnlyList<RiskMatrix>> Handle(
        GetAllRiskMatricesQuery query,
        CancellationToken cancellationToken = default) =>
        riskMatrixRepository.ListOrderedAsync(cancellationToken);

    public Task<IReadOnlyList<ComplianceMetrics>> Handle(
        GetAllComplianceMetricsQuery query,
        CancellationToken cancellationToken = default) =>
        complianceMetricsRepository.ListOrderedAsync(cancellationToken);
}
