using vantagePMO_platform.Profiles.Domain.Model.Queries;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Profiles.Application.QueryServices;

namespace vantagePMO_platform.Profiles.Application.Internal.QueryServices;

public class ProfileStatsQueryService(IProfileStatsRepository profileStatsRepository) : IProfileStatsQueryService
{
    public async Task<Domain.Model.Aggregates.ProfileStats?> Handle(
        GetProfileStatsByUserIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await profileStatsRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Model.Aggregates.ProfileStats>> Handle(
        GetAllProfileStatsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await profileStatsRepository.ListOrderedAsync(cancellationToken);
    }
}
