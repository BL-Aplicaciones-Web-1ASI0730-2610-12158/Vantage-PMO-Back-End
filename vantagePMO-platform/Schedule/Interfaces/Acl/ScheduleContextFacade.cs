using vantagePMO_platform.Schedule.Domain.Repositories;

namespace vantagePMO_platform.Schedule.Interfaces.Acl;

/// <summary>
///     Implementation of the Schedule bounded context ACL (Anti-Corruption Layer) facade.
///     Provides external interfaces for other bounded contexts to interact with Schedule.
/// </summary>
public class ScheduleContextFacade : IScheduleContextFacade
{
    private readonly IScheduleRepository _repository;

    public ScheduleContextFacade(IScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<object>> GetUserSchedulesAsync(int userId,
        CancellationToken cancellationToken = default)
    {
        var schedules = await _repository.FindByUserIdAsync(userId, cancellationToken);
        return schedules.Cast<object>();
    }

    public async Task<IEnumerable<object>> GetUserSchedulesInRangeAsync(int userId, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken = default)
    {
        var schedules = await _repository.FindByUserIdAndDateRangeAsync(userId, startDate, endDate, cancellationToken);
        return schedules.Cast<object>();
    }

    public async Task<object?> GetScheduleAsync(int scheduleId, CancellationToken cancellationToken = default)
    {
        var schedule = await _repository.FindByIdAsync(scheduleId, cancellationToken);
        return schedule;
    }

    public async Task<bool> IsScheduleActiveAsync(int scheduleId, CancellationToken cancellationToken = default)
    {
        var schedule = await _repository.FindByIdAsync(scheduleId, cancellationToken);
        return schedule?.Active ?? false;
    }
}
