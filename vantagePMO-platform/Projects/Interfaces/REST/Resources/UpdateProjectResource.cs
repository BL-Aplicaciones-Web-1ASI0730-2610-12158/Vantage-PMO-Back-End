namespace vantagePMO_platform.Projects.Interfaces.REST.Resources;

public record UpdateProjectResource(
    string Name,
    string? Category,
    string? Description,
    int? Progress,
    string? Status,
    string? StartDate,
    string? EndDate,
    string? DueDate,
    string? Manager,
    int? UserId,
    IEnumerable<TeamMemberResource>? TeamMembers,
    IEnumerable<MilestoneResource>? Milestones);
