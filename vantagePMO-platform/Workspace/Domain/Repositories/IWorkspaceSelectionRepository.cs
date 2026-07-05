using vantagePMO_platform.Workspace.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Workspace.Domain.Repositories;

public interface IWorkspaceSelectionRepository : IBaseRepository<WorkspaceSelection>
{
    Task<WorkspaceSelection?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}
