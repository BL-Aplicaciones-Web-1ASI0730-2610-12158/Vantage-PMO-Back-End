namespace vantagePMO_platform.Projects.Domain.Model.ValueObjects;

/// <summary>
///     Team member attached to a project (stored as JSON).
/// </summary>
public class TeamMember
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
}
