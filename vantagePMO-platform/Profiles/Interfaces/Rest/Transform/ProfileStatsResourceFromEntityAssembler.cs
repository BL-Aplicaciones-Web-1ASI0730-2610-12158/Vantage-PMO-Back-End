using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Profiles.Interfaces.Rest.Transform;

public static class ProfileStatsResourceFromEntityAssembler
{
    public static ProfileStatsResource ToResourceFromEntity(ProfileStats entity) =>
        new(
            entity.Id,
            entity.UserId,
            entity.TotalProjects,
            entity.OnTrack,
            entity.AtRisk,
            entity.Trend,
            entity.PortfolioHealth,
            entity.AttentionItems);
}
