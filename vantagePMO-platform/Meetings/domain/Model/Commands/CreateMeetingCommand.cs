namespace vantagePMO_platform.Meetings.Domain.Model.Commands;

/// <summary>
/// Comando que encapsula los datos necesarios para crear una reunión.
/// Es construido en la capa Interfaces (REST) a partir del recurso recibido
/// y consumido por el MeetingCommandService en Application.
/// </summary>
public record CreateMeetingCommand(
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
