using vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;
using vantagePMO_platform.RiskCompliance.Interfaces.Rest.Resources;

namespace vantagePMO_platform.RiskCompliance.Interfaces.Rest.Transform;

public static class RiskComplianceResourceFromEntityAssembler
{
    public static RiskResource ToResource(RiskItem entity) =>
        new(
            entity.Id,
            entity.RiskId,
            entity.Description,
            entity.Severity,
            entity.Likelihood,
            entity.Impact,
            entity.Status,
            entity.AuditTrail,
            entity.AuditDate,
            entity.FlaggedBy,
            entity.DaysOpen,
            entity.HasActionPlan,
            entity.Segment,
            entity.ControlLog);

    public static IEnumerable<RiskResource> ToResources(IEnumerable<RiskItem> entities) =>
        entities.Select(ToResource);

    public static RiskMatrixResource ToResource(RiskMatrix entity) =>
        new(
            entity.Id,
            entity.Segment,
            entity.Environment,
            entity.HeatmapCells
                .Select(cell => new HeatmapCellResource(cell.Impact, cell.Likelihood, cell.Value, cell.Level))
                .ToList());

    public static IEnumerable<RiskMatrixResource> ToResources(IEnumerable<RiskMatrix> entities) =>
        entities.Select(ToResource);

    public static ComplianceMetricsResource ToResource(ComplianceMetrics entity) =>
        new(
            entity.Id,
            entity.IntegrityPulse,
            entity.IntegrityDelta,
            entity.DocCompliance,
            entity.SlaCompliance,
            entity.SystemIntegrityAlerts
                .Select(alert => new SystemIntegrityAlertResource(
                    alert.Id,
                    alert.Code,
                    alert.Title,
                    alert.Description,
                    alert.Severity,
                    alert.Time,
                    alert.Status))
                .ToList());

    public static IEnumerable<ComplianceMetricsResource> ToResources(IEnumerable<ComplianceMetrics> entities) =>
        entities.Select(ToResource);
}
