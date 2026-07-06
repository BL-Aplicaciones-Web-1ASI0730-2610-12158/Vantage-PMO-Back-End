using vantagePMO_platform.Schedule.Application.CommandServices;
using vantagePMO_platform.Schedule.Domain.Model.Aggregates;
using vantagePMO_platform.Schedule.Domain.Model.Commands;
using vantagePMO_platform.Schedule.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Schedule.Application.Internal.CommandServices;

public class ScheduleCommandService(
    IScheduleRepository repository,
    IUnitOfWork unitOfWork) : IScheduleCommandService
{
    public async Task<int> CreateScheduleAsync(CreateScheduleCommand command,
        CancellationToken cancellationToken = default)
    {
        var scheduleEntry = new ScheduleEntry(command);
        scheduleEntry.CreatedAt = DateTimeOffset.UtcNow;
        scheduleEntry.UpdatedAt = DateTimeOffset.UtcNow;

        await repository.AddAsync(scheduleEntry, cancellationToken);
        await unitOfWork.CompleteAsync();

        return scheduleEntry.Id;
    }

    public async Task<bool> UpdateScheduleAsync(UpdateScheduleCommand command,
        CancellationToken cancellationToken = default)
    {
        var scheduleEntry = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (scheduleEntry == null)
            return false;

        scheduleEntry.Update(command);
        repository.Update(scheduleEntry);
        await unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteScheduleAsync(int id, CancellationToken cancellationToken = default)
    {
        var scheduleEntry = await repository.FindByIdAsync(id, cancellationToken);
        if (scheduleEntry == null)
            return false;

        repository.Remove(scheduleEntry);
        await unitOfWork.CompleteAsync();

        return true;
    }
}
