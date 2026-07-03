namespace vantagePMO_platform.Profiles.Domain.Model.Aggregates;

/// <summary>
///     Portfolio statistics associated with a user profile.
/// </summary>
public class ProfileStats
{
    protected ProfileStats()
    {
        PortfolioHealth = string.Empty;
    }

    public ProfileStats(
        int userId,
        int totalProjects,
        int onTrack,
        int atRisk,
        int trend,
        string portfolioHealth,
        int attentionItems)
    {
        UserId = userId;
        TotalProjects = totalProjects;
        OnTrack = onTrack;
        AtRisk = atRisk;
        Trend = trend;
        PortfolioHealth = portfolioHealth;
        AttentionItems = attentionItems;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int TotalProjects { get; private set; }
    public int OnTrack { get; private set; }
    public int AtRisk { get; private set; }
    public int Trend { get; private set; }
    public string PortfolioHealth { get; private set; }
    public int AttentionItems { get; private set; }
}
