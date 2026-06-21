using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Meetings.Interfaces.Rest.Transform;

/// <summary>
/// Convierte una entidad Meeting (dominio) en un MeetingResource (lo que ve el front).
/// </summary>
public static class MeetingResourceFromEntityAssembler
{
    public static MeetingResource ToResourceFromEntity(Meeting entity)
    {
        return new MeetingResource(
            entity.Id,
            entity.Title,
            entity.Date,
            entity.Time,
            entity.Duration,
            entity.Location,
            entity.Type,
            entity.Status,
            entity.Organizer,
            entity.Attendees,
            entity.Description,
            entity.Minutes,
            entity.Agreements,
            entity.Segment);
    }
}
