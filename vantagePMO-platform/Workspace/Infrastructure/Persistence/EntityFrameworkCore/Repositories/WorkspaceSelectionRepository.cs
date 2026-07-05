using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Workspace.Domain.Model.Aggregates;
using vantagePMO_platform.Workspace.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace vantagePMO_platform.Workspace.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class WorkspaceSelectionRepository(AppDbContext context)
    : BaseRepository<WorkspaceSelection>(context), IWorkspaceSelectionRepository
{
    public async Task<WorkspaceSelection?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<WorkspaceSelection>()
            .FirstOrDefaultAsync(selection => selection.UserId == userId, cancellationToken);
    }
}
