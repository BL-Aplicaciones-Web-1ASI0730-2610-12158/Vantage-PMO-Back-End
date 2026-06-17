using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.ValueObjects;
using vantagePMO_platform.Projects.Interfaces.REST.Resources;

namespace vantagePMO_platform.Projects.Interfaces.REST.Transform;

public static class ProjectResourceFromEntityAssembler
{
    public static ProjectResource ToResourceFromEntity(Project entity) =>
        new(
            entity.Id,
            entity.Name,
            entity.Category,
            entity.Description,
            entity.Progress,
            entity.Status,
            entity.StartDate,
            entity.EndDate,
            entity.DueDate,
            entity.Manager,
            entity.UserId,
            entity.TeamMembers.Select(ToTeamMemberResource),
            entity.Milestones.Select(ToMilestoneResource));

    public static IEnumerable<ProjectResource> ToResourcesFromEntities(IEnumerable<Project> entities) =>
        entities.Select(ToResourceFromEntity);

    private static TeamMemberResource ToTeamMemberResource(TeamMember member) =>
        new(member.Id, member.Name, member.Avatar);

    private static MilestoneResource ToMilestoneResource(Milestone milestone) =>
        new(milestone.Id, milestone.Name, milestone.Date, milestone.Type);
}
