namespace vantagePMO_platform.Schedule.Domain.Model.Queries;

/// <summary>
///     Query to retrieve all schedules for a given user.
/// </summary>
public record GetSchedulesByUserIdQuery(int UserId);
