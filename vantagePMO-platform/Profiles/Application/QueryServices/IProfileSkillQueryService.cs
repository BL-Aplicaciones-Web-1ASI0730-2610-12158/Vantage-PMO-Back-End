using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.Queries;

namespace vantagePMO_platform.Profiles.Application.QueryServices;

public interface IProfileSkillQueryService
{
    Task<IReadOnlyList<ProfileSkill>> Handle(GetProfileSkillsByUserIdQuery query, CancellationToken cancellationToken = default);
}
