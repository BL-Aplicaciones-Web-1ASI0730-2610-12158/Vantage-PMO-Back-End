using vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;
using vantagePMO_platform.RiskCompliance.Domain.Model.ValueObjects;
using vantagePMO_platform.RiskCompliance.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.RiskCompliance.Application.Internal.CommandServices;

/// <summary>
///     Seeds risk &amp; compliance data when tables are empty (matches front-end db.json).
/// </summary>
public class RiskComplianceSampleDataSeeder(
    IRiskItemRepository riskItemRepository,
    IRiskMatrixRepository riskMatrixRepository,
    IComplianceMetricsRepository complianceMetricsRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (await riskItemRepository.AnyAsync(cancellationToken)
            || await riskMatrixRepository.AnyAsync(cancellationToken)
            || await complianceMetricsRepository.AnyAsync(cancellationToken))
        {
            return;
        }

        var risks = new[]
        {
            new RiskItem(
                "R-01",
                "Cloud API Vulnerability detected in Segment B staging environments. Unauthorized access potential via deprecated OAuth endpoint.",
                "CRITICAL", "POSSIBLE", "HIGH", "open",
                "Flagged by AI", "Jun 1", "Al Thorne", 12, true,
                "Segment B", "Triage initiated by Al Thorne · 329"),
            new RiskItem(
                "R-54",
                "Latency notice passing through non-compliant region. GDPR violation risk in cross-border transfers.",
                "CRITICAL", "POSSIBLE", "HIGH", "open",
                "Compliance Alert", "Jun 3", "Marcus Thorne", 9, true,
                "Segment B", "Compliance Alert · 601"),
            new RiskItem(
                "R-09",
                "Automated key rotation failed for Cluster-02. Master keys exceeding 90-day validity window.",
                "HIGH", "PROBABLE", "MEDIUM", "open",
                "Auto-retry Scheduled", "Jun 5", "System", 7, true,
                "Segment B", "Auto-retry Scheduled · 44"),
            new RiskItem(
                "R-22",
                "Third-party dependency with known CVE-2024-3571 vulnerability in production build pipeline.",
                "HIGH", "UNLIKELY", "HIGH", "open",
                "Security Scan", "Jun 7", "DevSecOps", 4, false,
                "Segment B", "Security Scan · 18"),
            new RiskItem(
                "R-37",
                "Incomplete audit trail for data deletion events. Missing confirmation logs for GDPR Article 17 compliance.",
                "MEDIUM", "POSSIBLE", "MEDIUM", "monitoring",
                "Manual Review", "Jun 8", "Compliance Team", 3, true,
                "Segment B", "Manual Review · 7"),
        };

        foreach (var risk in risks)
            await riskItemRepository.AddAsync(risk, cancellationToken);

        var heatmapCells = new[]
        {
            new HeatmapCell("HIGH", "REMOTE", 4, "medium"),
            new HeatmapCell("HIGH", "UNLIKELY", 9, "medium"),
            new HeatmapCell("HIGH", "POSSIBLE", 3, "high"),
            new HeatmapCell("HIGH", "PROBABLE", 8, "high"),
            new HeatmapCell("HIGH", "CERTAIN", 11, "critical"),
            new HeatmapCell("MEDIUM", "REMOTE", 16, "low"),
            new HeatmapCell("MEDIUM", "UNLIKELY", 5, "low"),
            new HeatmapCell("MEDIUM", "POSSIBLE", 7, "medium"),
            new HeatmapCell("MEDIUM", "PROBABLE", 9, "high"),
            new HeatmapCell("MEDIUM", "CERTAIN", 6, "high"),
            new HeatmapCell("LOW", "REMOTE", 32, "low"),
            new HeatmapCell("LOW", "UNLIKELY", 22, "low"),
            new HeatmapCell("LOW", "POSSIBLE", 33, "low"),
            new HeatmapCell("LOW", "PROBABLE", 7, "low"),
            new HeatmapCell("LOW", "CERTAIN", 5, "medium"),
        };

        await riskMatrixRepository.AddAsync(
            new RiskMatrix("Segment B", "LIVE ENVIRONMENT", heatmapCells),
            cancellationToken);

        var alerts = new[]
        {
            new SystemIntegrityAlert(
                "N-1.01", "N1-14", "Node: Anomaly Detection",
                "Burst traffic from 199.86.124 — 3σ deviation detected. Auto-throttle engaged.",
                "HIGH", "11:42", "active"),
            new SystemIntegrityAlert(
                "Q1-14", "Q1-14", "Audit: Firewall Rule Mod",
                "Inbound rule modified via LiveEnvironment: 172.x IPv6 Transition. Logged in audit trail.",
                "MEDIUM", "10:31", "active"),
            new SystemIntegrityAlert(
                "G1-4", "G1-4", "NextBoot: Database Sync",
                "Secondary application database update cycle committed. Delta 0.1 committed.",
                "LOW", "09:11", "resolved"),
        };

        await complianceMetricsRepository.AddAsync(
            new ComplianceMetrics(98.4, 0.2, 94, 100, alerts),
            cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
