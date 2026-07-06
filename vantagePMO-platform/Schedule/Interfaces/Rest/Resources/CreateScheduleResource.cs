namespace vantagePMO_platform.Schedule.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new schedule via API.
/// </summary>
public record CreateScheduleResource(
    int UserId,
    DateTime Date,
    TimeOnly Time,
    int Duration,
    string Title,
    string Detail,
    string Type,
    bool Active);
