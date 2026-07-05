using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Meetings.Interfaces.Rest.Transform;

public static class MeetingResourceFromEntityAssembler
{
    public static MeetingResource ToResourceFromEntity(Meeting entity) =>
        new(
            entity.Id,
            entity.Title,
            entity.Segment,
            entity.Date.ToString("dd-MM-yyyy"),
            entity.Time.ToString("HH:mm"),
            entity.Duration,
            entity.Location,
            entity.Type,
            entity.Status,
            entity.Organizer,
            entity.Attendees,
            entity.Description,
            entity.Minutes.Select(minute => new MeetingMinuteResource(
                minute.Id,
                minute.Time,
                minute.Title,
                minute.Body)).ToList(),
            entity.Agreements.Select(agreement => new MeetingAgreementResource(
                agreement.Id,
                agreement.Title,
                agreement.Owner,
                agreement.Deadline,
                agreement.Tag,
                agreement.Status,
                agreement.Verified,
                agreement.Note,
                agreement.TaskRef)).ToList());

    public static IEnumerable<MeetingResource> ToResourcesFromEntities(IEnumerable<Meeting> entities) =>
        entities.Select(ToResourceFromEntity);
}


