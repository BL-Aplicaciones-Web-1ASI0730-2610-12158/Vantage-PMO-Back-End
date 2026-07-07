namespace vantagePMO_platform.ResourcePlanning.Interfaces.Rest.Resources;

public record PlanningSummaryResource(
    int TotalResources,
    int AvgUtilization,
    int OverAllocated,
    int BenchAvailable);

public record DepartmentUtilizationResource(string Department, int Utilization, string Status);

public record ProjectAllocationResource(string Name, int Allocation);

public record ResourceAllocationResource(
    int Id,
    string ResourceName,
    string Role,
    string Department,
    string Avatar,
    string AvatarColor,
    IEnumerable<ProjectAllocationResource> Projects,
    int TotalAllocation,
    string Status);

public record CapacityGapResource(
    int Id,
    string Project,
    string Role,
    string GapDate,
    int Headcount,
    string Severity);

public record ResourcePlanningDashboardResource(
    int Id,
    string Period,
    PlanningSummaryResource SummaryKpis,
    IEnumerable<DepartmentUtilizationResource> DepartmentCapacity,
    IEnumerable<ResourceAllocationResource> Allocations,
    IEnumerable<CapacityGapResource> CapacityGaps);
