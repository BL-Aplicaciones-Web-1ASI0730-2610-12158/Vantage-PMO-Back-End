using vantagePMO_platform.Schedule.Domain.Model.Aggregates;
using vantagePMO_platform.Schedule.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Schedule.Interfaces.Rest.Transform;

/// <summary>
///     Assembler for converting Schedule aggregates to API resources.
/// </summary>
public class ScheduleResourceFromEntityAssembler
{
    public static ScheduleResource ToResource(ScheduleEntry entity)
    {
        return new ScheduleResource(
            entity.Id,
            entity.UserId,
            entity.Date,
            entity.Time,
            entity.Duration,
            entity.Title,
            entity.Detail,
            entity.Type,
            entity.Active,
            entity.CreatedAt ?? DateTimeOffset.MinValue,
            entity.UpdatedAt ?? DateTimeOffset.MinValue);
    }

    public static IEnumerable<ScheduleResource> ToResources(IEnumerable<ScheduleEntry> entities)
    {
        return entities.Select(ToResource);
    }
}
