using vantagePMO_platform.Meetings.Domain.Model.Commands;
using vantagePMO_platform.Meetings.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Meetings.Domain.Model.Aggregates;

/// <summary>
///     Meeting aggregate root for the Meetings bounded context.
/// </summary>
public class Meeting
{
    protected Meeting()
    {
        Title = string.Empty;
        Location = string.Empty;
        Type = string.Empty;
        Status = string.Empty;
        Organizer = string.Empty;
        Attendees = new List<string>();
        Description = string.Empty;
        Minutes = new List<MeetingMinuteItem>();
        Agreements = new List<MeetingAgreementItem>();
        Segment = string.Empty;
    }

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
        Attendees = command.Attendees?.ToList() ?? new List<string>();
        Description = command.Description ?? string.Empty;
        Minutes = command.Minutes?.ToList() ?? new List<MeetingMinuteItem>();
        Agreements = command.Agreements?.ToList() ?? new List<MeetingAgreementItem>();
        Segment = command.Segment ?? string.Empty;
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly Time { get; private set; }
    public int Duration { get; private set; }
    public string Location { get; private set; }
    public string Type { get; private set; }
    public string Status { get; private set; }
    public string Organizer { get; private set; }
    public List<string> Attendees { get; private set; }
    public string Description { get; private set; }
    public List<MeetingMinuteItem> Minutes { get; private set; }
    public List<MeetingAgreementItem> Agreements { get; private set; }
    public string Segment { get; private set; }
}
