using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Queries;

namespace vantagePMO_platform.Profiles.Domain.Services;

public interface IProfileStatsQueryService
{
    Task<ProfileStats?> Handle(GetProfileStatsByUserIdQuery query, CancellationToken cancellationToken = default);
}
