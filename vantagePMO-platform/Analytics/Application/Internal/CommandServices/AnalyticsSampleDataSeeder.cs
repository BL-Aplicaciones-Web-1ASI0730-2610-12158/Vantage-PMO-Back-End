using vantagePMO_platform.Analytics.Domain.Model.Aggregates;
using vantagePMO_platform.Analytics.Domain.Model.ValueObjects;
using vantagePMO_platform.Analytics.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Analytics.Application.Internal.CommandServices;

/// <summary>
///     Seeds analytics dashboard sample data when the table is empty.
/// </summary>
public class AnalyticsSampleDataSeeder(
    IAnalyticsDashboardRepository analyticsDashboardRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (await analyticsDashboardRepository.AnyAsync(cancellationToken))
            return;

        await analyticsDashboardRepository.AddAsync(
            new AnalyticsDashboard(
                [
                    new MonthlyExpenditure("JAN", 62, 55),
                    new MonthlyExpenditure("MAR", 72, 65),
                    new MonthlyExpenditure("MAY", 85, 78),
                    new MonthlyExpenditure("JUL", 92, 85),
                    new MonthlyExpenditure("SEP", 98, 90),
                    new MonthlyExpenditure("NOV", 100, 93)
                ],
                new PortfolioRoi(72, "HIGH EFFICIENCY", 12_400_000, 14_100_000),
                [
                    new DepartmentCapacity("Engineering", 94, "warning"),
                    new DepartmentCapacity("Product Design", 78, "normal"),
                    new DepartmentCapacity("Data Science", 112, "over"),
                    new DepartmentCapacity("Strategy & Ops", 45, "normal")
                ],
                [
                    new TopMover("PX-882", "Cloud Migration", "Infrastructure", 14.2, "Optimal"),
                    new TopMover("PX-901", "Data Lake 2.0", "Storage", -8.4, "AtRisk"),
                    new TopMover("PX-711", "Mobile App V3", "Client Dev", 2.1, "Stable"),
                    new TopMover("PX-450", "B2B Integration", "Partnerships", 19.8, "Optimal")
                ],
                new SummaryKpis(
                    1_450_000,
                    "Available for Q3 expansion",
                    1.4,
                    12,
                    "Low",
                    3)),
            cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
