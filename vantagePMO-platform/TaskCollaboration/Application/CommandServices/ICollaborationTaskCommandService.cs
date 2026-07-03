using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Commands;

namespace vantagePMO_platform.TaskCollaboration.Application.CommandServices;

public interface ICollaborationTaskCommandService
{
    Task<CollaborationTask?> CreateAsync(CreateCollaborationTaskCommand command, CancellationToken cancellationToken = default);
    Task<CollaborationTask?> UpdateAsync(UpdateCollaborationTaskCommand command, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int taskId, CancellationToken cancellationToken = default);
}
