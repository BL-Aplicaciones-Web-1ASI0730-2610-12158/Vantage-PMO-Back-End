using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.Commands;
using vantagePMO_platform.Meetings.Domain.Model.ValueObjects;
using vantagePMO_platform.Meetings.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Meetings.Application.Internal.CommandServices;

/// <summary>
///     Seeds meetings when the table is empty (matches front-end db.json).
/// </summary>
public class MeetingsSampleDataSeeder(
    IMeetingRepository meetingRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        var existing = await meetingRepository.ListAsync(cancellationToken);
        if (existing.Any())
            return;

        var meetings = new[]
        {
            new Meeting(new CreateMeetingCommand(
                "Q3 Strategic Resource Allocation",
                new DateOnly(2026, 5, 14),
                new TimeOnly(9, 0),
                45,
                "Conf Room A",
                "Strategic",
                "Active",
                "Alex Sterling",
                ["Alex Sterling", "Sarah Johnson", "Marcus Lee", "Emily Watson", "Daniel Brooks"],
                "Reviewing engineering capacity across all Segment 1 initiatives.",
                [
                    new MeetingMinuteItem { Id = 1, Time = "09:05", Title = "Introductions", Body = "Marcus opened the floor confirming the $2.4M Q3 budget cap." },
                    new MeetingMinuteItem { Id = 2, Time = "09:15", Title = "Vantage Status", Body = "Sarah reported a 3-week delay due to infrastructure bottleneck in West-2 region." },
                    new MeetingMinuteItem { Id = 3, Time = "09:30", Title = "Resource Shift", Body = "Agreement reached to pivot 3 DevOps engineers from Maintenance to Vantage." },
                    new MeetingMinuteItem { Id = 4, Time = "09:45", Title = "Hiring", Body = "Executive board confirmed hiring freeze, but allowed one Critical Replacement role." }
                ],
                [
                    new MeetingAgreementItem { Id = 1, Title = "Pivot 3 DevOps engineers to Project Vantage", Owner = "Sarah J.", Deadline = "May 30", Tag = "Task", Status = "open" },
                    new MeetingAgreementItem { Id = 2, Title = "Cap Q3 variable spending at $2.4M across all units", Owner = "Finance Dept", Verified = "Board", Tag = "Policy", Status = "open" },
                    new MeetingAgreementItem { Id = 3, Title = "Maintain full hiring freeze until end of Q3", Owner = "Marcus T.", Note = "Policy Change", Tag = "Policy", Status = "open" },
                    new MeetingAgreementItem { Id = 4, Title = "Release memo regarding H2 2026 strategy", Owner = "Alex S.", TaskRef = "#4492", Tag = "Task", Status = "converted" }
                ],
                "Segment 1 (Leaders) Executive Board")),
            new Meeting(new CreateMeetingCommand(
                "Project Vantage Risk Assessment",
                new DateOnly(2026, 5, 12),
                new TimeOnly(14, 0),
                60,
                "Virtual",
                "Review",
                "Completed",
                "Sarah Johnson",
                ["Sarah Johnson", "Daniel Brooks", "Olivia Carter", "Marcus Lee"],
                "Deep dive into technical risks for Project Vantage Phase 2.",
                [
                    new MeetingMinuteItem { Id = 1, Time = "14:00", Title = "Risk Register Review", Body = "Daniel walked through the updated risk register with 3 high-priority items." },
                    new MeetingMinuteItem { Id = 2, Time = "14:20", Title = "Data Migration Blocker", Body = "Olivia presented a 2-week delay risk in the data migration pipeline due to schema mismatches." },
                    new MeetingMinuteItem { Id = 3, Time = "14:45", Title = "Mitigation Plan", Body = "Team agreed to allocate 2 additional backend engineers to resolve schema issues before June 1." }
                ],
                [
                    new MeetingAgreementItem { Id = 1, Title = "Allocate 2 backend engineers to data migration", Owner = "Sarah J.", Deadline = "Jun 1", Tag = "Task", Status = "open" },
                    new MeetingAgreementItem { Id = 2, Title = "Update risk register weekly during Phase 2", Owner = "Daniel B.", Tag = "Policy", Status = "open" }
                ],
                "Segment 2 (Operations) Engineering Leadership")),
            new Meeting(new CreateMeetingCommand(
                "Monthly Stakeholder Sync",
                new DateOnly(2026, 5, 9),
                new TimeOnly(11, 0),
                90,
                "Board Room",
                "Sync",
                "Completed",
                "Alex Sterling",
                ["Alex Sterling", "Sarah Johnson", "Michael Chen", "Patricia Lee", "Emma Watson", "Marcus Lee", "Daniel Brooks", "Sophia Miller"],
                "Monthly cross-segment alignment on portfolio health, budget status, and upcoming milestones.",
                [
                    new MeetingMinuteItem { Id = 1, Time = "11:00", Title = "Portfolio Health", Body = "Overall portfolio health rated Healthy with 2 at-risk projects flagged." },
                    new MeetingMinuteItem { Id = 2, Time = "11:25", Title = "Budget Review", Body = "Q2 budget utilization at 74%. Finance confirmed Q3 allocations are approved." },
                    new MeetingMinuteItem { Id = 3, Time = "11:50", Title = "Upcoming Milestones", Body = "5 major milestones due in the next 30 days across 3 projects." }
                ],
                [
                    new MeetingAgreementItem { Id = 1, Title = "Review at-risk projects weekly with PMs", Owner = "Alex S.", Tag = "Task", Status = "open" }
                ],
                "All Segments"))
        };

        foreach (var meeting in meetings)
            await meetingRepository.AddAsync(meeting, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
