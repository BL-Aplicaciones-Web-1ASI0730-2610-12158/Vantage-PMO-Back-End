namespace vantagePMO_platform.RiskCompliance.Domain.Model.ValueObjects;

public record SystemIntegrityAlert(
    string Id,
    string Code,
    string Title,
    string Description,
    string Severity,
    string Time,
    string Status);
