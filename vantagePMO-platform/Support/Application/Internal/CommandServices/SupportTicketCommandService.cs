using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.Support.Application.CommandServices;
using vantagePMO_platform.Support.Domain.Model.Aggregates;
using vantagePMO_platform.Support.Domain.Model.Commands;
using vantagePMO_platform.Support.Domain.Repositories;

namespace vantagePMO_platform.Support.Application.Internal.CommandServices;

public class SupportTicketCommandService(
    ISupportTicketRepository supportTicketRepository,
    IUnitOfWork unitOfWork) : ISupportTicketCommandService
{
    public async Task<SupportTicket?> CreateAsync(
        CreateSupportTicketCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            return null;

        var ticket = new SupportTicket(command);
        await supportTicketRepository.AddAsync(ticket, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ticket;
    }

    public async Task<SupportTicket?> UpdateAsync(
        UpdateSupportTicketCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            return null;

        var ticket = await supportTicketRepository.FindByIdAsync(command.TicketId, cancellationToken);
        if (ticket is null)
            return null;

        ticket.Update(command);
        supportTicketRepository.Update(ticket);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ticket;
    }
}
