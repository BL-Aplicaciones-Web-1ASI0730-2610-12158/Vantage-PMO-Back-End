namespace vantagePMO_platform.ResourcePlanning.Domain.Model.ValueObjects;

public record CapacityGap(int Id, string Project, string Role, string GapDate, int Headcount, string Severity);
