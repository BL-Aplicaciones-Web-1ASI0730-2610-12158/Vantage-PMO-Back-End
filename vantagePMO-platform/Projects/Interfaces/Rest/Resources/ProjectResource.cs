namespace vantagePMO_platform.Projects.Interfaces.Rest.Resources;

public record ProjectResource(
    int Id,
    string Name,
    string Category,
    string Description,
    int Progress,
    string Status,
    string StartDate,
    string EndDate,
    string DueDate,
    string Manager,
    int UserId,
    IEnumerable<TeamMemberResource> TeamMembers,
    IEnumerable<MilestoneResource> Milestones);
