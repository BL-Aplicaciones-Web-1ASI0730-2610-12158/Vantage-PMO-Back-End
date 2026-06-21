using vantagePMO_platform.Analytics.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Analytics.Domain.Model.Aggregates;

/// <summary>
///     Portfolio analytics dashboard aggregate.
/// </summary>
public class AnalyticsDashboard
{
    protected AnalyticsDashboard()
    {
        MonthlyExpenditures = new List<MonthlyExpenditure>();
        PortfolioRoi = new PortfolioRoi(0, string.Empty, 0, 0);
        ResourceSaturation = new List<DepartmentCapacity>();
        TopMovers = new List<TopMover>();
        SummaryKpis = new SummaryKpis(0, string.Empty, 0, 0, string.Empty, 0);
    }

    public AnalyticsDashboard(
        IEnumerable<MonthlyExpenditure> monthlyExpenditures,
        PortfolioRoi portfolioRoi,
        IEnumerable<DepartmentCapacity> resourceSaturation,
        IEnumerable<TopMover> topMovers,
        SummaryKpis summaryKpis)
    {
        MonthlyExpenditures = monthlyExpenditures.ToList();
        PortfolioRoi = portfolioRoi;
        ResourceSaturation = resourceSaturation.ToList();
        TopMovers = topMovers.ToList();
        SummaryKpis = summaryKpis;
    }

    public int Id { get; private set; }
    public List<MonthlyExpenditure> MonthlyExpenditures { get; private set; }
    public PortfolioRoi PortfolioRoi { get; private set; }
    public List<DepartmentCapacity> ResourceSaturation { get; private set; }
    public List<TopMover> TopMovers { get; private set; }
    public SummaryKpis SummaryKpis { get; private set; }
}
