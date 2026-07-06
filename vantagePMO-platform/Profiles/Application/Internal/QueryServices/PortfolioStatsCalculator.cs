using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.Aggregates;

namespace vantagePMO_platform.Profiles.Application.Internal.QueryServices;

/// <summary>
///     Derives portfolio statistics from a user's project list.
/// </summary>
public static class PortfolioStatsCalculator
{
    public static ProfileStats FromProjects(int userId, IReadOnlyList<Project> projects)
    {
        var totalProjects = projects.Count;
        var atRisk = projects.Count(project => IsAtRisk(project.Status));
        var onTrack = totalProjects - atRisk;
        var attentionItems = atRisk;
        var portfolioHealth = ResolvePortfolioHealth(totalProjects, atRisk);
        var trend = totalProjects == 0 ? 0 : (int)Math.Round(onTrack * 100.0 / totalProjects);

        return new ProfileStats(userId, totalProjects, onTrack, atRisk, trend, portfolioHealth, attentionItems);
    }

    private static bool IsAtRisk(string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return false;

        var normalized = status.Trim().ToLowerInvariant();
        return normalized is "at risk" or "at-risk" or "warning" or "critical";
    }

    private static string ResolvePortfolioHealth(int totalProjects, int atRisk)
    {
        if (totalProjects == 0 || atRisk == 0)
            return "Healthy";

        return atRisk >= Math.Ceiling(totalProjects / 2.0) ? "At Risk" : "Moderate";
    }
}
