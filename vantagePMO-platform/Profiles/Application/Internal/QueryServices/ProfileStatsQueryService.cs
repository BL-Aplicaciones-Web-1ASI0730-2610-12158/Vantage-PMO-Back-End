using vantagePMO_platform.Profiles.Domain.Model.Queries;
using vantagePMO_platform.Profiles.Application.QueryServices;
using vantagePMO_platform.Projects.Domain.Repositories;

namespace vantagePMO_platform.Profiles.Application.Internal.QueryServices;

public class ProfileStatsQueryService(IProjectRepository projectRepository) : IProfileStatsQueryService
{
    public async Task<Domain.Model.Aggregates.ProfileStats?> Handle(
        GetProfileStatsByUserIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var projects = await projectRepository.FindByUserIdAsync(query.UserId, cancellationToken);
        return PortfolioStatsCalculator.FromProjects(query.UserId, projects);
    }

    public async Task<IReadOnlyList<Domain.Model.Aggregates.ProfileStats>> Handle(
        GetAllProfileStatsQuery query,
        CancellationToken cancellationToken = default)
    {
        var projects = await projectRepository.ListOrderedAsync(cancellationToken);
        var userIds = projects
            .Select(project => project.UserId)
            .Distinct()
            .OrderBy(userId => userId);

        return userIds
            .Select(userId => PortfolioStatsCalculator.FromProjects(
                userId,
                projects.Where(project => project.UserId == userId).ToList()))
            .ToList();
    }
}
