namespace vantagePMO_platform.Schedule.Application.QueryServices;

using vantagePMO_platform.Shared.Domain.Model.Entities;

/// <summary>
///     Query service for retrieving schedule data.
/// </summary>
public interface IScheduleQueryService
{
    /// <summary>
    ///     Gets a schedule by its identifier.
    /// </summary>
    /// <param name="id">The schedule id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The schedule resource or null if not found.</returns>
    Task<object?> GetScheduleByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all schedules for a user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Collection of schedule resources.</returns>
    Task<IEnumerable<object>> GetSchedulesByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets schedules for a user within a date range.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="startDate">Start date filter.</param>
    /// <param name="endDate">End date filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Collection of schedule resources.</returns>
    Task<IEnumerable<object>> GetSchedulesByUserIdAndDateRangeAsync(int userId, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken = default);
}