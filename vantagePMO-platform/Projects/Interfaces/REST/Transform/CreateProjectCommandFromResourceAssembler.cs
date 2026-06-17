using vantagePMO_platform.Projects.Domain.Model.Commands;
using vantagePMO_platform.Projects.Domain.Model.ValueObjects;
using vantagePMO_platform.Projects.Interfaces.REST.Resources;

namespace vantagePMO_platform.Projects.Interfaces.REST.Transform;

public static class CreateProjectCommandFromResourceAssembler
{
    public static CreateProjectCommand ToCommandFromResource(CreateProjectResource resource) =>
        new(
            resource.Name,
            resource.Category,
            resource.Description,
            resource.Progress ?? 0,
            resource.Status,
            resource.StartDate,
            resource.EndDate,
            resource.DueDate,
            resource.Manager,
            resource.UserId ?? 0,
            ToTeamMembers(resource.TeamMembers),
            ToMilestones(resource.Milestones));

    private static List<TeamMember>? ToTeamMembers(IEnumerable<TeamMemberResource>? members) =>
        members?.Select(member => new TeamMember
        {
            Id = member.Id,
            Name = member.Name,
            Avatar = member.Avatar
        }).ToList();

    private static List<Milestone>? ToMilestones(IEnumerable<MilestoneResource>? milestones) =>
        milestones?.Select(milestone => new Milestone
        {
            Id = milestone.Id,
            Name = milestone.Name,
            Date = milestone.Date,
            Type = milestone.Type
        }).ToList();
}
