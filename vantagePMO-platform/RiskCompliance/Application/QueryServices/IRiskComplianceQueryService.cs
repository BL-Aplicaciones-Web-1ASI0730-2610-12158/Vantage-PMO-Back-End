using vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;
using vantagePMO_platform.RiskCompliance.Domain.Model.Queries;

namespace vantagePMO_platform.RiskCompliance.Application.QueryServices;

public interface IRiskComplianceQueryService
{
    Task<IReadOnlyList<RiskItem>> Handle(GetAllRisksQuery query, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RiskMatrix>> Handle(GetAllRiskMatricesQuery query, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ComplianceMetrics>> Handle(
        GetAllComplianceMetricsQuery query,
        CancellationToken cancellationToken = default);
}
