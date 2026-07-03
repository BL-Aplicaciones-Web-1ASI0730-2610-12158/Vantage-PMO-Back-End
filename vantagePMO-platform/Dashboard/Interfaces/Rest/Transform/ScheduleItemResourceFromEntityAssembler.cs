using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Dashboard.Interfaces.Rest.Transform;

public static class ScheduleItemResourceFromEntityAssembler
{
    public static ScheduleItemResource ToResourceFromEntity(ScheduleItem entity) =>
        new(
            entity.Id,
            entity.Date,
            entity.Time,
            entity.Duration,
            entity.Title,
            entity.Detail,
            entity.Type,
            entity.Active);

    public static IEnumerable<ScheduleItemResource> ToResourcesFromEntities(IEnumerable<ScheduleItem> entities) =>
        entities.Select(ToResourceFromEntity);
}
