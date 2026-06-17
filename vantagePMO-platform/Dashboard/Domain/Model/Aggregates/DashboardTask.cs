namespace vantagePMO_platform.Dashboard.Domain.Model.Aggregates;

/// <summary>
///     Priority task shown on the home dashboard.
/// </summary>
public class DashboardTask
{
    protected DashboardTask()
    {
        Title = string.Empty;
        Assignee = string.Empty;
        Department = string.Empty;
        Priority = string.Empty;
        Icon = string.Empty;
        IconBg = string.Empty;
        AvatarSeeds = new List<string>();
    }

    public DashboardTask(
        string title,
        string assignee,
        string department,
        string priority,
        string icon,
        string iconBg,
        IEnumerable<string>? avatarSeeds)
    {
        Title = title;
        Assignee = assignee;
        Department = department;
        Priority = priority;
        Icon = icon;
        IconBg = iconBg;
        AvatarSeeds = avatarSeeds?.ToList() ?? new List<string>();
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Assignee { get; private set; }
    public string Department { get; private set; }
    public string Priority { get; private set; }
    public string Icon { get; private set; }
    public string IconBg { get; private set; }
    public List<string> AvatarSeeds { get; private set; }
}
