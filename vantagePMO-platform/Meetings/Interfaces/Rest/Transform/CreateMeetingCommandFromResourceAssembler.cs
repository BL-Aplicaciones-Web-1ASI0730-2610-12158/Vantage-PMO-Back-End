using vantagePMO_platform.Meetings.Domain.Model.Commands;
using vantagePMO_platform.Meetings.Domain.Model.ValueObjects;
using vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Meetings.Interfaces.Rest.Transform;

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