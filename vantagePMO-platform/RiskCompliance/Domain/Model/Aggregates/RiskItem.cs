namespace vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;

public class RiskItem
{
    protected RiskItem()
    {
        RiskId = string.Empty;
        Description = string.Empty;
        Severity = string.Empty;
        Likelihood = string.Empty;
        Impact = string.Empty;
        Status = string.Empty;
        AuditTrail = string.Empty;
        AuditDate = string.Empty;
        FlaggedBy = string.Empty;
        Segment = string.Empty;
        ControlLog = string.Empty;
    }

    public RiskItem(
        string riskId,
        string description,
        string severity,
        string likelihood,
        string impact,
        string status,
        string auditTrail,
        string auditDate,
        string flaggedBy,
        int daysOpen,
        bool hasActionPlan,
        string segment,
        string controlLog) : this()
    {
        RiskId = riskId;
        Description = description;
        Severity = severity;
        Likelihood = likelihood;
        Impact = impact;
        Status = status;
        AuditTrail = auditTrail;
        AuditDate = auditDate;
        FlaggedBy = flaggedBy;
        DaysOpen = daysOpen;
        HasActionPlan = hasActionPlan;
        Segment = segment;
        ControlLog = controlLog;
    }

    public int Id { get; private set; }
    public string RiskId { get; private set; }
    public string Description { get; private set; }
    public string Severity { get; private set; }
    public string Likelihood { get; private set; }
    public string Impact { get; private set; }
    public string Status { get; private set; }
    public string AuditTrail { get; private set; }
    public string AuditDate { get; private set; }
    public string FlaggedBy { get; private set; }
    public int DaysOpen { get; private set; }
    public bool HasActionPlan { get; private set; }
    public string Segment { get; private set; }
    public string ControlLog { get; private set; }
}
