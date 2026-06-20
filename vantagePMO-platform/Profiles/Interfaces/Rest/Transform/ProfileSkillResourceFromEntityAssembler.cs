using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Profiles.Interfaces.Rest.Transform;

public static class ProfileSkillResourceFromEntityAssembler
{
    public static ProfileSkillResource ToResourceFromEntity(ProfileSkill entity) =>
        new(entity.Id, entity.UserId, entity.Name, entity.Percentage);

    public static IEnumerable<ProfileSkillResource> ToResourcesFromEntities(IEnumerable<ProfileSkill> entities) =>
        entities.Select(ToResourceFromEntity);
}
