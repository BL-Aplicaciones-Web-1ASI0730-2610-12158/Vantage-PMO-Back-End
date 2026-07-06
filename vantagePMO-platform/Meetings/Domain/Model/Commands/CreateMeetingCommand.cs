using vantagePMO_platform.Meetings.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Meetings.Domain.Model.Commands;

public record CreateMeetingCommand(
    string Title,
    DateOnly Date,
    TimeOnly Time,
    int Duration,
    string Location,
    string Type,
    string Status,
    string Organizer,
    List<string>? Attendees,
    string? Description,
    List<MeetingMinuteItem>? Minutes,
    List<MeetingAgreementItem>? Agreements,
    string? Segment);
