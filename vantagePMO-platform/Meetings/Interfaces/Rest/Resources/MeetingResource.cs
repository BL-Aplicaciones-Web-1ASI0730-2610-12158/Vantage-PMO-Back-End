namespace vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

/// <summary>
/// DTO de salida que se envía al front-end (Vue) en getAllMeetings y createMeeting.
/// Refleja exactamente los atributos del modelo Meeting del front.
/// </summary>
public record MeetingResource(
    int Id,
    string Title,
    DateOnly Date,
    TimeOnly Time,
    string Duration,
    string Location,
    string Type,
    string Status,
    string Organizer,
    List<string> Attendees,
    string Description,
    List<string> Minutes,
    List<string> Agreements,
    string Segment);
