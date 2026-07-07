namespace vantagePMO_platform.RiskCompliance.Interfaces.Rest.Resources;

public record RiskResource(
    int Id,
    string RiskId,
    string Description,
    string Severity,
    string Likelihood,
    string Impact,
    string Status,
    string AuditTrail,
    string AuditDate,
    string FlaggedBy,
    int DaysOpen,
    bool HasActionPlan,
    string Segment,
    string ControlLog);

public record HeatmapCellResource(string Impact, string Likelihood, int Value, string Level);

public record RiskMatrixResource(
    int Id,
    string Segment,
    string Environment,
    List<HeatmapCellResource> HeatmapCells);

public record SystemIntegrityAlertResource(
    string Id,
    string Code,
    string Title,
    string Description,
    string Severity,
    string Time,
    string Status);

public record ComplianceMetricsResource(
    int Id,
    double IntegrityPulse,
    double IntegrityDelta,
    int DocCompliance,
    int SlaCompliance,
    List<SystemIntegrityAlertResource> SystemIntegrityAlerts);
