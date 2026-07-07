namespace vantagePMO_platform.ResourcePlanning.Domain.Model.ValueObjects;

public record ResourceAllocation(
    int Id,
    string ResourceName,
    string Role,
    string Department,
    string Avatar,
    string AvatarColor,
    List<ProjectAllocation> Projects,
    int TotalAllocation,
    string Status);
