namespace vantagePMO_platform.Schedule.Interfaces.Rest.Resources;

/// <summary>
///     Resource for updating an existing schedule via API.
/// </summary>
public record UpdateScheduleResource(
    DateTime? Date,
    TimeOnly? Time,
    int? Duration,
    string? Title,
    string? Detail,
    string? Type,
    bool? Active);
