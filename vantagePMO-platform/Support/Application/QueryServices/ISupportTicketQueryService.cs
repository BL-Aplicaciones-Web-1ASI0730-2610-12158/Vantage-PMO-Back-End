using vantagePMO_platform.Support.Domain.Model.Aggregates;
using vantagePMO_platform.Support.Domain.Model.Queries;

namespace vantagePMO_platform.Support.Application.QueryServices;

public interface ISupportTicketQueryService
{
    Task<IEnumerable<SupportTicket>> Handle(GetAllSupportTicketsQuery query, CancellationToken cancellationToken = default);
}
