using vantagePMO_platform.Reports.Domain.Model.Aggregates;
using vantagePMO_platform.Reports.Domain.Repositories;
using VantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Reports.Application.Internal.CommandServices;

/// <summary>
///     Seeds Quick Reports sample data when the reports table is empty.
/// </summary>
public class ReportsSampleDataSeeder(
    IReportRepository reportRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (await reportRepository.AnyAsync(cancellationToken))
            return;

        await reportRepository.AddRangeAsync(
        [
            new Report(
                "Metropolitan Transit Expansion",
                "A1",
                "Sarah Jenkins",
                "Healthy",
                78,
                "Q3 FY24",
                88.4,
                4.2,
                12,
                2,
                10,
                -1.2,
                320,
                12,
                "2026-05-01",
                "metro-transit.pdf",
                "Health & Risk Assessment",
                [42, 55, 48, 62, 70, 68, 80],
                "Current trajectories indicate a strong finish for the quarter, though the Skyline project requires immediate resource reallocation to mitigate material shortage delays. Budget remains within architectural variance tolerances."),
            new Report(
                "Skyline Residential Hub",
                "B4",
                "David Chen",
                "Critical",
                42,
                "Q3 FY24",
                61.2,
                -3.1,
                7,
                4,
                3,
                -4.8,
                540,
                20,
                "2026-05-02",
                "skyline-hub.pdf",
                "Financial & Resource Allocation",
                [60, 45, 50, 40, 38, 35, 42],
                "Resource bottlenecks in structural engineering are causing delays. Recommend immediate escalation and contractor reallocation from Neo-Nexus to stabilize critical path."),
            new Report(
                "Smart Energy Control",
                "C2",
                "Alex Sterling",
                "Healthy",
                91,
                "Q3 FY24",
                94.1,
                6.8,
                3,
                0,
                3,
                0.4,
                210,
                9,
                "2026-05-03",
                "smart-energy.pdf",
                "Progress & Milestone Tracking",
                [55, 65, 72, 80, 85, 90, 94],
                "All milestones on track. Velocity is accelerating ahead of schedule. Consider reallocating surplus capacity to support Skyline residential hub."),
            new Report(
                "IoT Security Monitoring",
                "D7",
                "Marcus Lee",
                "Warning",
                57,
                "Q3 FY24",
                73.5,
                -1.2,
                9,
                1,
                8,
                -2.1,
                410,
                15,
                "2026-05-04",
                "iot-security.pdf",
                "Health & Risk Assessment",
                [50, 52, 48, 55, 50, 53, 57],
                "Security audit delays are impacting milestone delivery. Risk exposure is increasing. Suggest activating contingency plan B for penetration testing timeline.")
        ],
            cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
