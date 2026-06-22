using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProfileSkillRepository(AppDbContext context) : IProfileSkillRepository
{
    public async Task<IReadOnlyList<ProfileSkill>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await context.Set<ProfileSkill>()
            .Where(skill => skill.UserId == userId)
            .OrderBy(skill => skill.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ProfileSkill> skills, CancellationToken cancellationToken = default)
    {
        await context.Set<ProfileSkill>().AddRangeAsync(skills, cancellationToken);
    }
}
