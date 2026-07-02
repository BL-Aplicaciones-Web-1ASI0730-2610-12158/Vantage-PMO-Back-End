using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.Support.Domain.Model.Aggregates;

namespace vantagePMO_platform.Support.Domain.Repositories;

public interface ISupportTicketRepository : IBaseRepository<SupportTicket>
{
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
