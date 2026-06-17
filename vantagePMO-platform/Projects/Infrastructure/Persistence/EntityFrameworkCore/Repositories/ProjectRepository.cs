using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace vantagePMO_platform.Projects.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProjectRepository(AppDbContext context)
    : BaseRepository<Project>(context), IProjectRepository
{
    public async Task<IReadOnlyList<Project>> ListOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Project>()
            .OrderBy(project => project.Id)
            .ToListAsync(cancellationToken);
    }
}
