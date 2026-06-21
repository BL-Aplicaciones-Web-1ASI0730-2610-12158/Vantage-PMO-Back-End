using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Analytics.Domain.Model.Aggregates;
using vantagePMO_platform.Analytics.Domain.Repositories;
using VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AnalyticsDashboardRepository(AppDbContext context) : IAnalyticsDashboardRepository
{
    public async Task<IReadOnlyList<AnalyticsDashboard>> ListOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<AnalyticsDashboard>()
            .OrderBy(dashboard => dashboard.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AnalyticsDashboard dashboard, CancellationToken cancellationToken = default)
    {
        await context.Set<AnalyticsDashboard>().AddAsync(dashboard, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<AnalyticsDashboard>().AnyAsync(cancellationToken);
    }
}
