using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Domain.Model.Queries;

namespace vantagePMO_platform.Dashboard.Domain.Services;

public interface IScheduleItemQueryService
{
    Task<IReadOnlyList<ScheduleItem>> Handle(
        GetAllScheduleItemsQuery query,
        CancellationToken cancellationToken = default);
}
