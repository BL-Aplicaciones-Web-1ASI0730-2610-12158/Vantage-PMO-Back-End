using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Dashboard.Interfaces.Rest.Transform;

public static class DepartmentResourceFromEntityAssembler
{
    public static DepartmentResource ToResourceFromEntity(Department entity) =>
        new(entity.Id, entity.Name, entity.Percent);

    public static IEnumerable<DepartmentResource> ToResourcesFromEntities(IEnumerable<Department> entities) =>
        entities.Select(ToResourceFromEntity);
}
