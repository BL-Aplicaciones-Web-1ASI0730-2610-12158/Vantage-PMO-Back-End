using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DashboardTaskRepository(AppDbContext context) : IDashboardTaskRepository
{
    public async Task<IReadOnlyList<DashboardTask>> ListOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<DashboardTask>()
            .OrderBy(task => task.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<DashboardTask> tasks, CancellationToken cancellationToken = default)
    {
        await context.Set<DashboardTask>().AddRangeAsync(tasks, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<DashboardTask>().AnyAsync(cancellationToken);
    }
}
