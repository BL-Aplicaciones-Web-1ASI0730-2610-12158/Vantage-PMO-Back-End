using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.TaskCollaboration.Application.CommandServices;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Commands;
using vantagePMO_platform.TaskCollaboration.Domain.Repositories;

namespace vantagePMO_platform.TaskCollaboration.Application.Internal.CommandServices;

public class CollaborationTaskCommandService(
    ICollaborationTaskRepository collaborationTaskRepository,
    IUnitOfWork unitOfWork) : ICollaborationTaskCommandService
{
    public async Task<CollaborationTask?> CreateAsync(
        CreateCollaborationTaskCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            return null;

        var task = new CollaborationTask(command);
        await collaborationTaskRepository.AddAsync(task, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return task;
    }

    public async Task<CollaborationTask?> UpdateAsync(
        UpdateCollaborationTaskCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            return null;

        var task = await collaborationTaskRepository.FindByIdAsync(command.TaskId, cancellationToken);
        if (task is null)
            return null;

        task.Update(command);
        collaborationTaskRepository.Update(task);
        await unitOfWork.CompleteAsync(cancellationToken);
        return task;
    }

    public async Task<bool> DeleteAsync(int taskId, CancellationToken cancellationToken = default)
    {
        var task = await collaborationTaskRepository.FindByIdAsync(taskId, cancellationToken);
        if (task is null)
            return false;

        collaborationTaskRepository.Remove(task);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}
