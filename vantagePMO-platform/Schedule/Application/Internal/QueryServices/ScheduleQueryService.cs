using vantagePMO_platform.Schedule.Application.QueryServices;
using vantagePMO_platform.Schedule.Domain.Repositories;
using vantagePMO_platform.Schedule.Interfaces.Rest.Transform;

namespace vantagePMO_platform.Schedule.Application.Internal.QueryServices;

public class ScheduleQueryService : IScheduleQueryService
{
    private readonly IScheduleRepository _repository;

    public ScheduleQueryService(IScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> GetScheduleByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var scheduleEntry = await _repository.FindByIdAsync(id, cancellationToken);
        return scheduleEntry != null ? ScheduleResourceFromEntityAssembler.ToResource(scheduleEntry) : null;
    }

    public async Task<IEnumerable<object>> GetSchedulesByUserIdAsync(int userId,
        CancellationToken cancellationToken = default)
    {
        var schedules = await _repository.FindByUserIdAsync(userId, cancellationToken);
        return ScheduleResourceFromEntityAssembler.ToResources(schedules).Cast<object>();
    }

    public async Task<IEnumerable<object>> GetSchedulesByUserIdAndDateRangeAsync(int userId, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken = default)
    {
        var schedules =
            await _repository.FindByUserIdAndDateRangeAsync(userId, startDate, endDate, cancellationToken);
        return ScheduleResourceFromEntityAssembler.ToResources(schedules).Cast<object>();
    }
}
