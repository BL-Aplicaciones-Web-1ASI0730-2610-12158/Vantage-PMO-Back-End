using vantagePMO_platform.Profiles.Domain.Model.Aggregates;

namespace vantagePMO_platform.Profiles.Domain.Repositories;

public interface IProfileSkillRepository
{
    Task<IReadOnlyList<ProfileSkill>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ProfileSkill> skills, CancellationToken cancellationToken = default);
}
