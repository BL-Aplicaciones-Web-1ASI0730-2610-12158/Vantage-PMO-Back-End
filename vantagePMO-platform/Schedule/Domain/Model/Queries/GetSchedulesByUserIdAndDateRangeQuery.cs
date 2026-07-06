namespace vantagePMO_platform.Schedule.Domain.Model.Queries;

/// <summary>
///     Query to retrieve schedules for a user within a date range.
/// </summary>
public record GetSchedulesByUserIdAndDateRangeQuery(int UserId, DateTime StartDate, DateTime EndDate);
