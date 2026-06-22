namespace vantagePMO_platform.Analytics.Domain.Model.ValueObjects;

public record SummaryKpis(
    long UnallocatedFunds,
    string UnallocatedSubtext,
    double VelocityIndex,
    int VelocityChange,
    string RiskExposure,
    int MitigationPlans);
