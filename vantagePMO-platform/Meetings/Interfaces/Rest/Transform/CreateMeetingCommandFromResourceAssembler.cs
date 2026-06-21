using vantagePMO_platform.Meetings.Domain.Model.Commands;
using vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Meetings.Interfaces.Rest.Transform;

/// <summary>
/// Convierte un CreateMeetingResource (lo que llega del front) en un
/// CreateMeetingCommand (lo que entiende el dominio/aplicación).
/// </summary>
public static class CreateMeetingCommandFromResourceAssembler
{
    public static CreateMeetingCommand ToCommandFromResource(CreateMeetingResource resource)
    {
        return new CreateMeetingCommand(
            resource.Title,
            resource.Date,
            resource.Time,
            resource.Duration,
            resource.Location,
            resource.Type,
            resource.Status,
            resource.Organizer,
            resource.Attendees,
            resource.Description,
            resource.Minutes,
            resource.Agreements,
            resource.Segment);
    }
}
