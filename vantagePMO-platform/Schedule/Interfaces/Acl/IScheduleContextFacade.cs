namespace vantagePMO_platform.Schedule.Interfaces.Acl;

/// <summary>
///     Public interface for the Schedule bounded context ACL (Anti-Corruption Layer).
///     Other bounded contexts use this to interact with Schedule.
/// </summary>
public interface IScheduleContextFacade
{
    /// <summary>
    ///     Retrieves all schedules for a user.
    /// </summary>
    Task<IEnumerable<object>> GetUserSchedulesAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Retrieves schedules for a user within a date range.
    /// </summary>
    Task<IEnumerable<object>> GetUserSchedulesInRangeAsync(int userId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Retrieves a specific schedule by id.
    /// </summary>
    Task<object?> GetScheduleAsync(int scheduleId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks if a schedule is active.
    /// </summary>
    Task<bool> IsScheduleActiveAsync(int scheduleId, CancellationToken cancellationToken = default);
}
