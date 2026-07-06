namespace vantagePMO_platform.Schedule.Domain.Model.Commands;

/// <summary>
///     Command carrying all data required to create a new schedule.
/// </summary>
public record CreateScheduleCommand(
    int UserId,
    DateTime Date,
    TimeOnly Time,
    int Duration,
    string Title,
    string Detail,
    string Type,
    bool Active);
