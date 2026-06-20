using vantagePMO_platform.Profiles.Domain.Model.Queries;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Profiles.Application.QueryServices;

namespace vantagePMO_platform.Profiles.Application.Internal.QueryServices;

public class ProfileSkillQueryService(IProfileSkillRepository profileSkillRepository) : IProfileSkillQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.ProfileSkill>> Handle(
        GetProfileSkillsByUserIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await profileSkillRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }
}
