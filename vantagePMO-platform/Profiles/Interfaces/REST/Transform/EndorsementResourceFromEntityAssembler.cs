using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Interfaces.REST.Resources;

namespace vantagePMO_platform.Profiles.Interfaces.REST.Transform;

public static class EndorsementResourceFromEntityAssembler
{
    public static EndorsementResource ToResourceFromEntity(Endorsement entity) =>
        new(
            entity.Id,
            entity.UserId,
            entity.Quote,
            entity.AuthorName,
            entity.AuthorRole,
            entity.AuthorAvatarSeed);

    public static IEnumerable<EndorsementResource> ToResourcesFromEntities(IEnumerable<Endorsement> entities) =>
        entities.Select(ToResourceFromEntity);
}
