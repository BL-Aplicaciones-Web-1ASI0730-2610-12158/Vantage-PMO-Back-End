namespace vantagePMO_platform.Meetings.Domain.Model.ValueObjects;

public class MeetingMinuteItem
{
    public int Id { get; set; }
    public string Time { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
