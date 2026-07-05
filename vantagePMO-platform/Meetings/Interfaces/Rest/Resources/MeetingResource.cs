namespace vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

public record MeetingResource(
    int Id,
    string Title,
    string Segment,
    string Date,
    string Time,
    int Duration,
    string Location,
    string Type,
    string Status,
    string Organizer,
    List<string> Attendees,
    string Description,
    List<MeetingMinuteResource> Minutes,
    List<MeetingAgreementResource> Agreements);