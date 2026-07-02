using vantagePMO_platform.Support.Domain.Model.Aggregates;
using vantagePMO_platform.Support.Domain.Model.Commands;
using vantagePMO_platform.Support.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Support.Application.Internal.CommandServices;

/// <summary>
///     Seeds support tickets when the table is empty (matches front-end db.json).
/// </summary>
public class SupportSampleDataSeeder(
    ISupportTicketRepository supportTicketRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (await supportTicketRepository.AnyAsync(cancellationToken))
            return;

        var tickets = new[]
        {
            new SupportTicket(new CreateSupportTicketCommand(
                "Cannot export PDF reports",
                "The export button in the Reports section does not generate a PDF file.",
                "Bug",
                "High",
                "Open",
                "2026-05-10",
                "Support Team")),
            new SupportTicket(new CreateSupportTicketCommand(
                "Dashboard loading slowly",
                "The home dashboard takes more than 10 seconds to load all widgets.",
                "Performance",
                "Medium",
                "In Progress",
                "2026-05-11",
                "Engineering")),
            new SupportTicket(new CreateSupportTicketCommand(
                "Add dark mode option",
                "Users have requested a dark mode theme for better accessibility.",
                "Feature Request",
                "Low",
                "Open",
                "2026-05-12",
                "Design Team")),
            new SupportTicket(new CreateSupportTicketCommand(
                "Email notifications not received",
                "Configured email alerts are not being delivered to user inboxes.",
                "Bug",
                "High",
                "Resolved",
                "2026-05-08",
                "Support Team")),
            new SupportTicket(new CreateSupportTicketCommand(
                "Integrate with Jira",
                "Request to connect Vantage PMO with Jira for issue tracking synchronization.",
                "Feature Request",
                "Medium",
                "Open",
                "2026-05-13",
                "Product Team")),
            new SupportTicket(new CreateSupportTicketCommand(
                "Risk",
                string.Empty,
                "Performance",
                "Low",
                "Open",
                "2026-05-14",
                "Support Team"))
        };

        foreach (var ticket in tickets)
            await supportTicketRepository.AddAsync(ticket, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
