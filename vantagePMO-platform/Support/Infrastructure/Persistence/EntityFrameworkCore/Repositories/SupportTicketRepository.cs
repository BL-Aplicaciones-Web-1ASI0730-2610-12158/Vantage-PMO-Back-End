using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Support.Domain.Model.Aggregates;
using vantagePMO_platform.Support.Domain.Repositories;

namespace vantagePMO_platform.Support.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupportTicketRepository(AppDbContext context)
    : BaseRepository<SupportTicket>(context), ISupportTicketRepository
{
    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<SupportTicket>().AnyAsync(cancellationToken);
    }
}
