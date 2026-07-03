namespace vantagePMO_platform.Dashboard.Interfaces.Rest.Resources;

public record DashboardTaskResource(
    int Id,
    string Title,
    string Assignee,
    string Department,
    string Priority,
    string Icon,
    string IconBg,
    IEnumerable<string> AvatarSeeds);
