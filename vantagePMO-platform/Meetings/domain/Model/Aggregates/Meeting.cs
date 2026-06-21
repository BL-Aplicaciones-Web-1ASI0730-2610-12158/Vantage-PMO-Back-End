using vantagePMO_platform.Meetings.Domain.Model.Commands;

namespace vantagePMO_platform.Meetings.Domain.Model.Aggregates;

/// <summary>
/// Aggregate root que representa una reunión (Meeting) dentro del bounded context Meetings.
/// Refleja los atributos definidos en el front-end (Vue):
/// id, title, date, time, duration, location, type, status, organizer,
/// attendees, description, minutes, agreements, segment.
/// </summary>
public class Meeting
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly Time { get; private set; }
    public string Duration { get; private set; }
    public string Location { get; private set; }
    public string Type { get; private set; }
    public string Status { get; private set; }
    public string Organizer { get; private set; }
    public List<string> Attendees { get; private set; }
    public string Description { get; private set; }
    public List<string> Minutes { get; private set; }
    public List<string> Agreements { get; private set; }
    public string Segment { get; private set; }

    /// <summary>Constructor protegido requerido por Entity Framework Core.</summary>
    protected Meeting()
    {
        Title = string.Empty;
        Duration = string.Empty;
        Location = string.Empty;
        Type = string.Empty;
        Status = string.Empty;
        Organizer = string.Empty;
        Attendees = new List<string>();
        Description = string.Empty;
        Minutes = new List<string>();
        Agreements = new List<string>();
        Segment = string.Empty;
    }

    /// <summary>Crea una nueva reunión a partir del comando de creación.</summary>
    public Meeting(CreateMeetingCommand command) : this()
    {
        Title = command.Title;
        Date = command.Date;
        Time = command.Time;
        Duration = command.Duration;
        Location = command.Location;
        Type = command.Type;
        Status = command.Status;
        Organizer = command.Organizer;
        Attendees = command.Attendees ?? new List<string>();
        Description = command.Description ?? string.Empty;
        Minutes = command.Minutes ?? new List<string>();
        Agreements = command.Agreements ?? new List<string>();
        Segment = command.Segment ?? string.Empty;
    }
}
