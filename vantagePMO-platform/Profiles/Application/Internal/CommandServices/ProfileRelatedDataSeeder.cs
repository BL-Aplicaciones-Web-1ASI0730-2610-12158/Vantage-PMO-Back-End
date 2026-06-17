using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Repositories;

namespace vantagePMO_platform.Profiles.Application.Internal.CommandServices;

/// <summary>
///     Seeds default profile-related data for a newly created user profile.
/// </summary>
public class ProfileRelatedDataSeeder(
    IProfileStatsRepository profileStatsRepository,
    IProfileSkillRepository profileSkillRepository,
    IEndorsementRepository endorsementRepository)
{
    public async Task SeedDefaultsAsync(int userId, string fullName, CancellationToken cancellationToken = default)
    {
        var firstName = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? fullName;

        await profileStatsRepository.AddAsync(
            new ProfileStats(userId, 0, 0, 0, 0, "Healthy", 0),
            cancellationToken);

        await profileSkillRepository.AddRangeAsync(
        [
            new ProfileSkill(userId, "Project Management", 75),
            new ProfileSkill(userId, "Stakeholder Communication", 70),
            new ProfileSkill(userId, "Risk Assessment", 65)
        ],
            cancellationToken);

        await endorsementRepository.AddRangeAsync(
        [
            new Endorsement(
                userId,
                $"{firstName} consistently delivers high-quality outcomes and communicates clearly across teams.",
                "Michael Chen",
                "Program Director",
                "Michael"),
            new Endorsement(
                userId,
                $"A reliable collaborator who brings structure and accountability to every initiative.",
                "Sarah Lopez",
                "Portfolio Manager",
                "Sarah")
        ],
            cancellationToken);
    }
}
