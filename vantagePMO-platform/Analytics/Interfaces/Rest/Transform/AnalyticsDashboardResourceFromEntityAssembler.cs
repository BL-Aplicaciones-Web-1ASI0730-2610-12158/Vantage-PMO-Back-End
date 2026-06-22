using vantagePMO_platform.Analytics.Domain.Model.Aggregates;
using vantagePMO_platform.Analytics.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Analytics.Interfaces.Rest.Transform;

public static class AnalyticsDashboardResourceFromEntityAssembler
{
    public static AnalyticsDashboardResource ToResourceFromEntity(AnalyticsDashboard entity) =>
        new(
            entity.Id,
            entity.MonthlyExpenditures.Select(item =>
                new MonthlyExpenditureResource(item.Month, item.Forecast, item.Actual)),
            new PortfolioRoiResource(
                entity.PortfolioRoi.Percentage,
                entity.PortfolioRoi.EfficiencyLabel,
                entity.PortfolioRoi.Target,
                entity.PortfolioRoi.Projected),
            entity.ResourceSaturation.Select(item =>
                new DepartmentCapacityResource(item.Department, item.Capacity, item.Status)),
            entity.TopMovers.Select(item =>
                new TopMoverResource(item.ProjectId, item.Name, item.Category, item.Delta, item.Health)),
            new SummaryKpisResource(
                entity.SummaryKpis.UnallocatedFunds,
                entity.SummaryKpis.UnallocatedSubtext,
                entity.SummaryKpis.VelocityIndex,
                entity.SummaryKpis.VelocityChange,
                entity.SummaryKpis.RiskExposure,
                entity.SummaryKpis.MitigationPlans));

    public static IEnumerable<AnalyticsDashboardResource> ToResourcesFromEntities(
        IEnumerable<AnalyticsDashboard> entities) =>
        entities.Select(ToResourceFromEntity);
}
