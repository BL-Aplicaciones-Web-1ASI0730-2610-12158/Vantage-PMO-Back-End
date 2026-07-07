using vantagePMO_platform.ResourcePlanning.Domain.Model.Aggregates;
using vantagePMO_platform.ResourcePlanning.Interfaces.Rest.Resources;

namespace vantagePMO_platform.ResourcePlanning.Interfaces.Rest.Transform;

public static class ResourcePlanningDashboardResourceFromEntityAssembler
{
    public static ResourcePlanningDashboardResource ToResourceFromEntity(ResourcePlanningDashboard entity) =>
        new(
            entity.Id,
            entity.Period,
            new PlanningSummaryResource(
                entity.SummaryKpis.TotalResources,
                entity.SummaryKpis.AvgUtilization,
                entity.SummaryKpis.OverAllocated,
                entity.SummaryKpis.BenchAvailable),
            entity.DepartmentCapacity.Select(item =>
                new DepartmentUtilizationResource(item.Department, item.Utilization, item.Status)),
            entity.Allocations.Select(item =>
                new ResourceAllocationResource(
                    item.Id,
                    item.ResourceName,
                    item.Role,
                    item.Department,
                    item.Avatar,
                    item.AvatarColor,
                    item.Projects.Select(project =>
                        new ProjectAllocationResource(project.Name, project.Allocation)),
                    item.TotalAllocation,
                    item.Status)),
            entity.CapacityGaps.Select(item =>
                new CapacityGapResource(
                    item.Id,
                    item.Project,
                    item.Role,
                    item.GapDate,
                    item.Headcount,
                    item.Severity)));

    public static IEnumerable<ResourcePlanningDashboardResource> ToResourcesFromEntities(
        IEnumerable<ResourcePlanningDashboard> entities) =>
        entities.Select(ToResourceFromEntity);
}
