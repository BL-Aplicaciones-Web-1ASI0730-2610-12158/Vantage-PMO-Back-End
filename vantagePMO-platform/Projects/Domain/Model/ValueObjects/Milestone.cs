namespace vantagePMO_platform.Projects.Domain.Model.ValueObjects;

/// <summary>
///     Milestone attached to a project (stored as JSON).
/// </summary>
public class Milestone
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Date { get; set; }
    public string Type { get; set; } = "pending";
}
