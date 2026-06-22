using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class EndorsementRepository(AppDbContext context) : IEndorsementRepository
{
    public async Task<IReadOnlyList<Endorsement>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await context.Set<Endorsement>()
            .Where(endorsement => endorsement.UserId == userId)
            .OrderBy(endorsement => endorsement.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Endorsement> endorsements, CancellationToken cancellationToken = default)
    {
        await context.Set<Endorsement>().AddRangeAsync(endorsements, cancellationToken);
    }
}
