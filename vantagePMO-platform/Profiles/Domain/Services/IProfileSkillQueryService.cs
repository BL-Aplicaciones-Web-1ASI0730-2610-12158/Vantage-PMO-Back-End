using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Queries;

namespace vantagePMO_platform.Profiles.Domain.Services;

public interface IProfileSkillQueryService
{
    Task<IReadOnlyList<ProfileSkill>> Handle(GetProfileSkillsByUserIdQuery query, CancellationToken cancellationToken = default);
}
