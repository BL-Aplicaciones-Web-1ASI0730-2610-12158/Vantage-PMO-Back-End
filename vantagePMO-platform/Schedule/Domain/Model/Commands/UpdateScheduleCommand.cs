namespace vantagePMO_platform.Schedule.Domain.Model.Commands;

/// <summary>
///     Command carrying data to update an existing schedule.
/// </summary>
public record UpdateScheduleCommand(
    int Id,
    DateTime? Date,
    TimeOnly? Time,
    int? Duration,
    string? Title,
    string? Detail,
    string? Type,
    bool? Active);
