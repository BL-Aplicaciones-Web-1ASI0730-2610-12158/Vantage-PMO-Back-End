using vantagePMO_platform.Profiles.Domain.Model.Aggregates;

namespace vantagePMO_platform.Profiles.Domain.Repositories;

public interface IEndorsementRepository
{
    Task<IReadOnlyList<Endorsement>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Endorsement> endorsements, CancellationToken cancellationToken = default);
}
