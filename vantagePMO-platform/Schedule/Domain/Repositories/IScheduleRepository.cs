using vantagePMO_platform.Schedule.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Schedule.Domain.Repositories;

/// <summary>
///     Repository abstraction for the <see cref="Schedule" /> aggregate.
/// </summary>
public interface IScheduleRepository : IBaseRepository<ScheduleEntry>
{
    /// <summary>
    ///     Finds a schedule entry by its identifier.
    /// </summary>
    /// <param name="id">The schedule id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The matching schedule entry, or <c>null</c> when none exists.</returns>
    new Task<ScheduleEntry?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds all schedules for a given user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Collection of user schedules.</returns>
    Task<IEnumerable<ScheduleEntry>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds all active schedules for a given user within a date range.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="startDate">Start date for filtering.</param>
    /// <param name="endDate">End date for filtering.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Collection of schedules within the date range.</returns>
    Task<IEnumerable<ScheduleEntry>> FindByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default);
}
