namespace vantagePMO_platform.Reports.Interfaces.Rest.Resources;

public record ReportResource(
    int Id,
    string Project,
    string Label,
    string Manager,
    string Status,
    int Completion,
    string Period,
    double AggregateHealth,
    double HealthTrend,
    int ActiveRisks,
    int CriticalRisks,
    int MinorRisks,
    double BudgetVariance,
    int EnergyConsumption,
    int Automations,
    string GeneratedAt,
    string Attachment,
    string Type,
    IEnumerable<int> VelocityTrend,
    string AiInsight);
