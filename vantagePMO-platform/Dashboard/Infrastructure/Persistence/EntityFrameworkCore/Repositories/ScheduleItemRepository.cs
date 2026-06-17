using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ScheduleItemRepository(AppDbContext context) : IScheduleItemRepository
{
    public async Task<IReadOnlyList<ScheduleItem>> ListOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ScheduleItem>()
            .OrderBy(item => item.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ScheduleItem> items, CancellationToken cancellationToken = default)
    {
        await context.Set<ScheduleItem>().AddRangeAsync(items, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ScheduleItem>().AnyAsync(cancellationToken);
    }
}
