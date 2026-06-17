using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Interfaces.REST.Resources;

namespace vantagePMO_platform.Dashboard.Interfaces.REST.Transform;

public static class DashboardTaskResourceFromEntityAssembler
{
    public static DashboardTaskResource ToResourceFromEntity(DashboardTask entity) =>
        new(
            entity.Id,
            entity.Title,
            entity.Assignee,
            entity.Department,
            entity.Priority,
            entity.Icon,
            entity.IconBg,
            entity.AvatarSeeds);

    public static IEnumerable<DashboardTaskResource> ToResourcesFromEntities(IEnumerable<DashboardTask> entities) =>
        entities.Select(ToResourceFromEntity);
}
