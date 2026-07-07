using vantagePMO_platform.RiskCompliance.Domain.Model.ValueObjects;

namespace vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;

public class ComplianceMetrics
{
    public const int SingletonId = 1;

    protected ComplianceMetrics()
    {
        SystemIntegrityAlerts = new List<SystemIntegrityAlert>();
    }

    public ComplianceMetrics(
        double integrityPulse,
        double integrityDelta,
        int docCompliance,
        int slaCompliance,
        IEnumerable<SystemIntegrityAlert> systemIntegrityAlerts) : this()
    {
        Id = SingletonId;
        IntegrityPulse = integrityPulse;
        IntegrityDelta = integrityDelta;
        DocCompliance = docCompliance;
        SlaCompliance = slaCompliance;
        SystemIntegrityAlerts = systemIntegrityAlerts.ToList();
    }

    public int Id { get; private set; }
    public double IntegrityPulse { get; private set; }
    public double IntegrityDelta { get; private set; }
    public int DocCompliance { get; private set; }
    public int SlaCompliance { get; private set; }
    public List<SystemIntegrityAlert> SystemIntegrityAlerts { get; private set; }
}
