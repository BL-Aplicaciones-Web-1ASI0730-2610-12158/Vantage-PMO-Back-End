namespace vantagePMO_platform.Reports.Domain.Model.Aggregates;

/// <summary>
///     Executive project performance report for Quick Reports (Segment 1).
/// </summary>
public class Report
{
    protected Report()
    {
        Project = string.Empty;
        Label = string.Empty;
        Manager = string.Empty;
        Status = string.Empty;
        Period = string.Empty;
        GeneratedAt = string.Empty;
        Attachment = string.Empty;
        Type = string.Empty;
        AiInsight = string.Empty;
        VelocityTrend = new List<int>();
    }

    public Report(
        string project,
        string label,
        string manager,
        string status,
        int completion,
        string period,
        double aggregateHealth,
        double healthTrend,
        int activeRisks,
        int criticalRisks,
        int minorRisks,
        double budgetVariance,
        int energyConsumption,
        int automations,
        string generatedAt,
        string attachment,
        string type,
        IEnumerable<int>? velocityTrend,
        string aiInsight)
    {
        Project = project;
        Label = label;
        Manager = manager;
        Status = status;
        Completion = completion;
        Period = period;
        AggregateHealth = aggregateHealth;
        HealthTrend = healthTrend;
        ActiveRisks = activeRisks;
        CriticalRisks = criticalRisks;
        MinorRisks = minorRisks;
        BudgetVariance = budgetVariance;
        EnergyConsumption = energyConsumption;
        Automations = automations;
        GeneratedAt = generatedAt;
        Attachment = attachment;
        Type = type;
        VelocityTrend = velocityTrend?.ToList() ?? new List<int>();
        AiInsight = aiInsight;
    }

    public int Id { get; private set; }
    public string Project { get; private set; }
    public string Label { get; private set; }
    public string Manager { get; private set; }
    public string Status { get; private set; }
    public int Completion { get; private set; }
    public string Period { get; private set; }
    public double AggregateHealth { get; private set; }
    public double HealthTrend { get; private set; }
    public int ActiveRisks { get; private set; }
    public int CriticalRisks { get; private set; }
    public int MinorRisks { get; private set; }
    public double BudgetVariance { get; private set; }
    public int EnergyConsumption { get; private set; }
    public int Automations { get; private set; }
    public string GeneratedAt { get; private set; }
    public string Attachment { get; private set; }
    public string Type { get; private set; }
    public List<int> VelocityTrend { get; private set; }
    public string AiInsight { get; private set; }
}
