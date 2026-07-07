using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.ResourcePlanning.Domain.Model.Aggregates;
using vantagePMO_platform.ResourcePlanning.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.ResourcePlanning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ResourcePlanningDashboardRepository(AppDbContext context) : IResourcePlanningDashboardRepository
{
    public async Task<IReadOnlyList<ResourcePlanningDashboard>> ListOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ResourcePlanningDashboard>()
            .OrderBy(dashboard => dashboard.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ResourcePlanningDashboard dashboard, CancellationToken cancellationToken = default)
    {
        await context.Set<ResourcePlanningDashboard>().AddAsync(dashboard, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ResourcePlanningDashboard>().AnyAsync(cancellationToken);
    }
}
