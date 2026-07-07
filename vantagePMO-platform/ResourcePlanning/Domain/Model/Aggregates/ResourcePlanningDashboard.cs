using vantagePMO_platform.ResourcePlanning.Domain.Model.ValueObjects;

namespace vantagePMO_platform.ResourcePlanning.Domain.Model.Aggregates;

/// <summary>
///     Resource planning dashboard aggregate.
/// </summary>
public class ResourcePlanningDashboard
{
    protected ResourcePlanningDashboard()
    {
        Period = string.Empty;
        SummaryKpis = new PlanningSummary(0, 0, 0, 0);
        DepartmentCapacity = new List<DepartmentUtilization>();
        Allocations = new List<ResourceAllocation>();
        CapacityGaps = new List<CapacityGap>();
    }

    public ResourcePlanningDashboard(
        string period,
        PlanningSummary summaryKpis,
        IEnumerable<DepartmentUtilization> departmentCapacity,
        IEnumerable<ResourceAllocation> allocations,
        IEnumerable<CapacityGap> capacityGaps)
    {
        Period = period;
        SummaryKpis = summaryKpis;
        DepartmentCapacity = departmentCapacity.ToList();
        Allocations = allocations.ToList();
        CapacityGaps = capacityGaps.ToList();
    }

    public int Id { get; private set; }
    public string Period { get; private set; }
    public PlanningSummary SummaryKpis { get; private set; }
    public List<DepartmentUtilization> DepartmentCapacity { get; private set; }
    public List<ResourceAllocation> Allocations { get; private set; }
    public List<CapacityGap> CapacityGaps { get; private set; }
}
