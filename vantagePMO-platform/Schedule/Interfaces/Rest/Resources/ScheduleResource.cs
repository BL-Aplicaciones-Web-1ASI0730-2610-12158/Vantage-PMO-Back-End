namespace vantagePMO_platform.Schedule.Interfaces.Rest.Resources;

/// <summary>
///     Resource for representing a schedule in API responses.
/// </summary>
public record ScheduleResource(
    int Id,
    int UserId,
    DateTime Date,
    TimeOnly Time,
    int Duration,
    string Title,
    string Detail,
    string Type,
    bool Active,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
