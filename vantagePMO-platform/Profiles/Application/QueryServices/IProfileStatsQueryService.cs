using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Queries;

namespace vantagePMO_platform.Profiles.Application.QueryServices;

public interface IProfileStatsQueryService
{
    Task<ProfileStats?> Handle(GetProfileStatsByUserIdQuery query, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProfileStats>> Handle(GetAllProfileStatsQuery query, CancellationToken cancellationToken = default);
}
