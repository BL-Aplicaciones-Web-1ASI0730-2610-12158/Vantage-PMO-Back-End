using vantagePMO_platform.Schedule.Domain.Model.Commands;

namespace vantagePMO_platform.Schedule.Application.CommandServices;

/// <summary>
///     Command service for handling schedule operations.
/// </summary>
public interface IScheduleCommandService
{
    /// <summary>
    ///     Creates a new schedule.
    /// </summary>
    /// <param name="command">The create command with schedule data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the created schedule.</returns>
    Task<int> CreateScheduleAsync(CreateScheduleCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates an existing schedule.
    /// </summary>
    /// <param name="command">The update command with modified schedule data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if update was successful, false if schedule not found.</returns>
    Task<bool> UpdateScheduleAsync(UpdateScheduleCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes a schedule.
    /// </summary>
    /// <param name="id">The schedule id to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deletion was successful, false if schedule not found.</returns>
    Task<bool> DeleteScheduleAsync(int id, CancellationToken cancellationToken = default);
}
