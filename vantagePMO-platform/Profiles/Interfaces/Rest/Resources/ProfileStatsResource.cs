namespace vantagePMO_platform.Profiles.Interfaces.Rest.Resources;

public record ProfileStatsResource(
    int Id,
    int UserId,
    int TotalProjects,
    int OnTrack,
    int AtRisk,
    int Trend,
    string PortfolioHealth,
    int AttentionItems);
