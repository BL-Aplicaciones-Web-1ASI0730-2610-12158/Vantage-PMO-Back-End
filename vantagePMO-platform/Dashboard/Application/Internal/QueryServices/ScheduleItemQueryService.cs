using vantagePMO_platform.Dashboard.Domain.Model.Queries;
using vantagePMO_platform.Dashboard.Domain.Repositories;
using vantagePMO_platform.Dashboard.Domain.Services;

namespace vantagePMO_platform.Dashboard.Application.Internal.QueryServices;

public class ScheduleItemQueryService(IScheduleItemRepository scheduleItemRepository) : IScheduleItemQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.ScheduleItem>> Handle(
        GetAllScheduleItemsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await scheduleItemRepository.ListOrderedAsync(cancellationToken);
    }
}
