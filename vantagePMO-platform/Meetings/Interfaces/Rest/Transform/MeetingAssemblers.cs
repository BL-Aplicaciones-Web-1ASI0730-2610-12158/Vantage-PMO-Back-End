using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.Commands;
using vantagePMO_platform.Meetings.Domain.Model.ValueObjects;
using vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Meetings.Interfaces.Rest.Transform;

public static class MeetingResourceFromEntityAssembler
{
    public static MeetingResource ToResourceFromEntity(Meeting entity) =>
        new(
            entity.Id,
            entity.Title,
            entity.Segment,
            entity.Date.ToString("yyyy-MM-dd"),
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

public static class CreateMeetingCommandFromResourceAssembler
{
    public static CreateMeetingCommand ToCommandFromResource(CreateMeetingResource resource) =>
        new(
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
            resource.Minutes?.Select(minute => new MeetingMinuteItem
            {
                Id = minute.Id,
                Time = minute.Time,
                Title = minute.Title,
                Body = minute.Body
            }).ToList(),
            resource.Agreements?.Select(agreement => new MeetingAgreementItem
            {
                Id = agreement.Id,
                Title = agreement.Title,
                Owner = agreement.Owner,
                Deadline = agreement.Deadline,
                Tag = agreement.Tag,
                Status = agreement.Status,
                Verified = agreement.Verified,
                Note = agreement.Note,
                TaskRef = agreement.TaskRef
            }).ToList(),
            resource.Segment);
}
