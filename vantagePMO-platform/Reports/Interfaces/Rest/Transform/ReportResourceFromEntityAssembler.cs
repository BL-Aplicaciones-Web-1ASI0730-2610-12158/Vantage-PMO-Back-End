using vantagePMO_platform.Reports.Domain.Model.Aggregates;
using vantagePMO_platform.Reports.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Reports.Interfaces.Rest.Transform;

public static class ReportResourceFromEntityAssembler
{
    public static ReportResource ToResourceFromEntity(Report entity) =>
        new(
            entity.Id,
            entity.Project,
            entity.Label,
            entity.Manager,
            entity.Status,
            entity.Completion,
            entity.Period,
            entity.AggregateHealth,
            entity.HealthTrend,
            entity.ActiveRisks,
            entity.CriticalRisks,
            entity.MinorRisks,
            entity.BudgetVariance,
            entity.EnergyConsumption,
            entity.Automations,
            entity.GeneratedAt,
            entity.Attachment,
            entity.Type,
            entity.VelocityTrend,
            entity.AiInsight);

    public static IEnumerable<ReportResource> ToResourcesFromEntities(IEnumerable<Report> entities) =>
        entities.Select(ToResourceFromEntity);
}
