namespace vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

public record MeetingMinuteResource(int Id, string Time, string Title, string Body);

public record MeetingAgreementResource(
    int Id,
    string Title,
    string Owner,
    string? Deadline,
    string? Tag,
    string? Status,
    string? Verified,
    string? Note,
    string? TaskRef);

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
