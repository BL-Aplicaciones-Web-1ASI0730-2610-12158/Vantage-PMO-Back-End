using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;

namespace vantagePMO_platform.Dashboard.Domain.Repositories;

public interface IScheduleItemRepository
{
    Task<IReadOnlyList<ScheduleItem>> ListOrderedAsync(CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ScheduleItem> items, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
