namespace vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

public record CreateMeetingResource(
    string Title,
    string Segment,
    DateOnly Date,
    TimeOnly Time,
    int Duration,
    string Location,
    string Type,
    string Status,
    string Organizer,
    List<string>? Attendees,
    string? Description,
    List<MeetingMinuteResource>? Minutes,
    List<MeetingAgreementResource>? Agreements);
