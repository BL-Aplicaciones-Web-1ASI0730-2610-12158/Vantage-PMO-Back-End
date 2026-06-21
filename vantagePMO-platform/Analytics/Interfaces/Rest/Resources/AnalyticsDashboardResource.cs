namespace vantagePMO_platform.Analytics.Interfaces.Rest.Resources;

public record MonthlyExpenditureResource(string Month, int Forecast, int Actual);

public record PortfolioRoiResource(
    int Percentage,
    string EfficiencyLabel,
    long Target,
    long Projected);

public record DepartmentCapacityResource(string Department, int Capacity, string Status);

public record TopMoverResource(
    string ProjectId,
    string Name,
    string Category,
    double Delta,
    string Health);

public record SummaryKpisResource(
    long UnallocatedFunds,
    string UnallocatedSubtext,
    double VelocityIndex,
    int VelocityChange,
    string RiskExposure,
    int MitigationPlans);

public record AnalyticsDashboardResource(
    int Id,
    IEnumerable<MonthlyExpenditureResource> MonthlyExpenditures,
    PortfolioRoiResource PortfolioRoi,
    IEnumerable<DepartmentCapacityResource> ResourceSaturation,
    IEnumerable<TopMoverResource> TopMovers,
    SummaryKpisResource SummaryKpis);
