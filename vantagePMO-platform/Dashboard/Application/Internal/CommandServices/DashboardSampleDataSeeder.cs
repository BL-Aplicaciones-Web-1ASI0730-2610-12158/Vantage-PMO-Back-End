using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Domain.Repositories;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Dashboard.Application.Internal.CommandServices;

/// <summary>
///     Seeds home dashboard data when tables are empty.
/// </summary>
public class DashboardSampleDataSeeder(
    IDashboardTaskRepository dashboardTaskRepository,
    IScheduleItemRepository scheduleItemRepository,
    IDepartmentRepository departmentRepository,
    IProfileStatsRepository profileStatsRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        var seeded = false;

        if (!await dashboardTaskRepository.AnyAsync(cancellationToken))
        {
            await dashboardTaskRepository.AddRangeAsync(
            [
                new DashboardTask(
                    "Budget reallocation for Q4 material procurement",
                    "John Doe",
                    "Finance",
                    "CRITICAL",
                    "pi pi-exclamation-triangle",
                    "#fef2f2",
                    ["John", "Marcus"]),
                new DashboardTask(
                    "Finalize Q3 Architectural Specs for Dubai Mall Expansion",
                    "Alex Sterling",
                    "Engineering",
                    "URGENT",
                    "pi pi-building",
                    "#eff6ff",
                    ["Alex"]),
                new DashboardTask(
                    "Client presentation for Sustainable Skyscraper concept",
                    "Sarah Jenkins",
                    "Design",
                    "MEDIUM",
                    "pi pi-palette",
                    "#f5f3ff",
                    ["Sarah"]),
                new DashboardTask(
                    "Environmental impact assessment for site Alpha",
                    "David L.",
                    "Engineering",
                    "NORMAL",
                    "pi pi-globe",
                    "#ecfdf5",
                    ["David"])
            ],
                cancellationToken);
            seeded = true;
        }

        if (!await scheduleItemRepository.AnyAsync(cancellationToken))
        {
            await scheduleItemRepository.AddRangeAsync(
            [
                new ScheduleItem("2026-05-11", "09:00", 45, "Stakeholder Briefing", "Conf Room A • 45m", "meeting", true),
                new ScheduleItem("2026-05-11", "14:00", 60, "Project Deep Dive", "Virtual • 1h", "work", true),
                new ScheduleItem("2026-05-12", "10:00", 30, "Design Review", "Conf Room B • 30m", "review", true),
                new ScheduleItem("2026-05-12", "15:00", 120, "Deep Work Session", "No distractions • 2h", "focus", false),
                new ScheduleItem("2026-05-13", "11:00", 45, "Sprint Retrospective", "Virtual • 45m", "meeting", true)
            ],
                cancellationToken);
            seeded = true;
        }

        if (!await departmentRepository.AnyAsync(cancellationToken))
        {
            await departmentRepository.AddRangeAsync(
            [
                new Department("Design", 92),
                new Department("Development", 74),
                new Department("Marketing", 58),
                new Department("Finance", 81)
            ],
                cancellationToken);
            seeded = true;
        }

        if (!await profileStatsRepository.AnyAsync(cancellationToken))
        {
            await profileStatsRepository.AddAsync(
                new ProfileStats(1, 24, 18, 3, 12, "Healthy", 2),
                cancellationToken);
            seeded = true;
        }

        if (seeded)
            await unitOfWork.CompleteAsync(cancellationToken);
    }
}
