using vantagePMO_platform.Support.Domain.Model.Aggregates;
using vantagePMO_platform.Support.Domain.Model.Commands;

namespace vantagePMO_platform.Support.Application.CommandServices;

public interface ISupportTicketCommandService
{
    Task<SupportTicket?> CreateAsync(CreateSupportTicketCommand command, CancellationToken cancellationToken = default);

    Task<SupportTicket?> UpdateAsync(UpdateSupportTicketCommand command, CancellationToken cancellationToken = default);
}
