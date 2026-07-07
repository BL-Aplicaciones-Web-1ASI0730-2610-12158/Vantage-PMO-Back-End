namespace vantagePMO_platform.ResourcePlanning.Domain.Model.ValueObjects;

public record PlanningSummary(int TotalResources, int AvgUtilization, int OverAllocated, int BenchAvailable);
