using vantagePMO_platform.Projects.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Projects.Domain.Model.Commands;

public record CreateProjectCommand(
    string Name,
    string? Category,
    string? Description,
    int Progress,
    string? Status,
    string? StartDate,
    string? EndDate,
    string? DueDate,
    string? Manager,
    int UserId,
    IEnumerable<TeamMember>? TeamMembers,
    IEnumerable<Milestone>? Milestones);
