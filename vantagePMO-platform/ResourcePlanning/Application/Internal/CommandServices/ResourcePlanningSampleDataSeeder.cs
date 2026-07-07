using vantagePMO_platform.ResourcePlanning.Domain.Model.Aggregates;
using vantagePMO_platform.ResourcePlanning.Domain.Model.ValueObjects;
using vantagePMO_platform.ResourcePlanning.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.ResourcePlanning.Application.Internal.CommandServices;

/// <summary>
///     Seeds resource planning dashboard sample data when the table is empty.
/// </summary>
public class ResourcePlanningSampleDataSeeder(
    IResourcePlanningDashboardRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (await repository.AnyAsync(cancellationToken))
            return;

        await repository.AddAsync(
            new ResourcePlanningDashboard(
                "Q3 2026",
                new PlanningSummary(48, 87, 3, 6),
                [
                    new DepartmentUtilization("Engineering", 94, "warning"),
                    new DepartmentUtilization("Product Design", 78, "normal"),
                    new DepartmentUtilization("Data Science", 112, "over"),
                    new DepartmentUtilization("Strategy & Ops", 45, "normal")
                ],
                [
                    new ResourceAllocation(
                        1, "Alex Sterling", "Lead Architect", "Engineering", "AS", "#3b82f6",
                        [new ProjectAllocation("Aura Mixed-Use", 60), new ProjectAllocation("Nexus Data Hub", 30)],
                        90, "optimal"),
                    new ResourceAllocation(
                        2, "Marcus R.", "Senior Engineer", "Engineering", "MR", "#f59e0b",
                        [new ProjectAllocation("Cloud Migration", 70), new ProjectAllocation("Mobile App V3", 45)],
                        115, "over"),
                    new ResourceAllocation(
                        3, "Diana K.", "UX Lead", "Product Design", "DK", "#8b5cf6",
                        [new ProjectAllocation("Aura Mixed-Use", 50), new ProjectAllocation("B2B Integration", 25)],
                        75, "optimal"),
                    new ResourceAllocation(
                        4, "Elena V.", "Data Scientist", "Data Science", "EV", "#ec4899",
                        [new ProjectAllocation("Data Lake 2.0", 80), new ProjectAllocation("Nexus Data Hub", 35)],
                        115, "over"),
                    new ResourceAllocation(
                        5, "Jordan P.", "PMO Analyst", "Strategy & Ops", "JP", "#10b981",
                        [new ProjectAllocation("Portfolio Reporting", 40)],
                        40, "bench"),
                    new ResourceAllocation(
                        6, "Catherine L.", "DevOps Engineer", "Engineering", "CL", "#06b6d4",
                        [new ProjectAllocation("Cloud Migration", 55), new ProjectAllocation("Nexus Data Hub", 50)],
                        105, "warning")
                ],
                [
                    new CapacityGap(1, "Cloud Migration", "Data Engineer", "Aug 12", 2, "high"),
                    new CapacityGap(2, "Aura Mixed-Use", "Structural Engineer", "Sep 01", 1, "medium"),
                    new CapacityGap(3, "B2B Integration", "Integration Specialist", "Oct 15", 1, "low")
                ]),
            cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
