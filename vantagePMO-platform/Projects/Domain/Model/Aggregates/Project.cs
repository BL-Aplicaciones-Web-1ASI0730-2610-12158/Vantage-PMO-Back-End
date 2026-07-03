using vantagePMO_platform.Projects.Domain.Model.Commands;
using vantagePMO_platform.Projects.Domain.Model.ValueObjects;
using vantagePMO_platform.Shared.Domain.Model.Entities;

namespace vantagePMO_platform.Projects.Domain.Model.Aggregates;

/// <summary>
///     Project aggregate root for the Projects bounded context.
/// </summary>
public class Project : IAuditableEntity
{
    protected Project()
    {
        Name = string.Empty;
        Category = string.Empty;
        Description = string.Empty;
        Status = string.Empty;
        StartDate = string.Empty;
        EndDate = string.Empty;
        DueDate = string.Empty;
        Manager = string.Empty;
        TeamMembers = new List<TeamMember>();
        Milestones = new List<Milestone>();
    }

    public Project(CreateProjectCommand command)
    {
        Name = command.Name.Trim();
        Category = command.Category?.Trim() ?? string.Empty;
        Description = command.Description?.Trim() ?? string.Empty;
        Progress = command.Progress;
        Status = command.Status?.Trim() ?? "healthy";
        StartDate = command.StartDate?.Trim() ?? string.Empty;
        EndDate = command.EndDate?.Trim() ?? string.Empty;
        DueDate = command.DueDate?.Trim() ?? string.Empty;
        Manager = command.Manager?.Trim() ?? string.Empty;
        UserId = command.UserId;
        TeamMembers = command.TeamMembers?.ToList() ?? new List<TeamMember>();
        Milestones = command.Milestones?.ToList() ?? new List<Milestone>();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Category { get; private set; }
    public string Description { get; private set; }
    public int Progress { get; private set; }
    public string Status { get; private set; }
    public string StartDate { get; private set; }
    public string EndDate { get; private set; }
    public string DueDate { get; private set; }
    public string Manager { get; private set; }
    public int UserId { get; private set; }
    public List<TeamMember> TeamMembers { get; private set; }
    public List<Milestone> Milestones { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Update(UpdateProjectCommand command)
    {
        Name = command.Name.Trim();
        Category = command.Category?.Trim() ?? string.Empty;
        Description = command.Description?.Trim() ?? string.Empty;
        Progress = command.Progress;
        Status = command.Status?.Trim() ?? Status;
        StartDate = command.StartDate?.Trim() ?? string.Empty;
        EndDate = command.EndDate?.Trim() ?? string.Empty;
        DueDate = command.DueDate?.Trim() ?? string.Empty;
        Manager = command.Manager?.Trim() ?? string.Empty;
        if (command.UserId.HasValue)
            UserId = command.UserId.Value;
        TeamMembers = command.TeamMembers?.ToList() ?? new List<TeamMember>();
        Milestones = command.Milestones?.ToList() ?? new List<Milestone>();
    }
}
