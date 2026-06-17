namespace vantagePMO_platform.Dashboard.Domain.Model.Aggregates;

/// <summary>
///     Schedule entry shown on the home dashboard.
/// </summary>
public class ScheduleItem
{
    protected ScheduleItem()
    {
        Date = string.Empty;
        Time = string.Empty;
        Title = string.Empty;
        Detail = string.Empty;
        Type = string.Empty;
    }

    public ScheduleItem(
        string date,
        string time,
        int duration,
        string title,
        string detail,
        string type,
        bool active)
    {
        Date = date;
        Time = time;
        Duration = duration;
        Title = title;
        Detail = detail;
        Type = type;
        Active = active;
    }

    public int Id { get; private set; }
    public string Date { get; private set; }
    public string Time { get; private set; }
    public int Duration { get; private set; }
    public string Title { get; private set; }
    public string Detail { get; private set; }
    public string Type { get; private set; }
    public bool Active { get; private set; }
}
