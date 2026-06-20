namespace vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

/// <summary>
/// DTO de entrada que recibe el body del POST /api/v1/meetings (createMeeting).
/// Attendees, Description, Minutes, Agreements y Segment son opcionales,
/// igual que en el constructor del front (?? [] / ?? '').
/// </summary>
public record CreateMeetingResource(
    string Title,
    DateOnly Date,
    TimeOnly Time,
    string Duration,
    string Location,
    string Type,
    string Status,
    string Organizer,
    List<string>? Attendees,
    string? Description,
    List<string>? Minutes,
    List<string>? Agreements,
    string? Segment);
