using vantagePMO_platform.Support.Application.QueryServices;
using vantagePMO_platform.Support.Domain.Model.Aggregates;
using vantagePMO_platform.Support.Domain.Model.Queries;
using vantagePMO_platform.Support.Domain.Repositories;

namespace vantagePMO_platform.Support.Application.Internal.QueryServices;

public class SupportTicketQueryService(ISupportTicketRepository supportTicketRepository) : ISupportTicketQueryService
{
    public async Task<IEnumerable<SupportTicket>> Handle(
        GetAllSupportTicketsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await supportTicketRepository.ListAsync(cancellationToken);
    }
}
