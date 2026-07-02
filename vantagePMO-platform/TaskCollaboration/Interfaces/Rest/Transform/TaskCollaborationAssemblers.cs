using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Commands;
using vantagePMO_platform.TaskCollaboration.Interfaces.Rest.Resources;

namespace vantagePMO_platform.TaskCollaboration.Interfaces.Rest.Transform;

public static class TaskCollaborationResourceFromEntityAssembler
{
    public static BoardResource ToResource(Board board) =>
        new(board.Id, board.Name, board.ProjectsActive, board.Description);

    public static IEnumerable<BoardResource> ToResources(IEnumerable<Board> boards) =>
        boards.Select(ToResource);

    public static BoardMemberResource ToResource(BoardMember member) =>
        new(member.Id, member.BoardId, member.Name, member.Role, member.Avatar, member.Color, member.Status);

    public static IEnumerable<BoardMemberResource> ToResources(IEnumerable<BoardMember> members) =>
        members.Select(ToResource);

    public static CollaborationTaskResource ToResource(CollaborationTask task) =>
        new(
            task.Id,
            task.BoardId,
            task.Project,
            task.Title,
            task.Description,
            task.Assignee,
            task.AssigneeAvatar,
            task.AssigneeColor,
            task.Status,
            task.Priority,
            task.PriorityColor,
            task.Completed,
            task.Comments,
            task.Attachments,
            task.DueDate,
            task.Progress,
            task.Department);

    public static IEnumerable<CollaborationTaskResource> ToResources(IEnumerable<CollaborationTask> tasks) =>
        tasks.Select(ToResource);
}

public static class CollaborationTaskCommandFromResourceAssembler
{
    public static CreateCollaborationTaskCommand ToCreateCommand(CreateCollaborationTaskResource resource) =>
        new(
            resource.BoardId,
            resource.Project,
            resource.Title,
            resource.Description,
            resource.Assignee,
            resource.AssigneeAvatar,
            resource.AssigneeColor,
            resource.Status,
            resource.Priority,
            resource.PriorityColor,
            resource.Completed,
            resource.Comments,
            resource.Attachments,
            resource.DueDate,
            resource.Progress,
            resource.Department);

    public static UpdateCollaborationTaskCommand ToUpdateCommand(int id, UpdateCollaborationTaskResource resource) =>
        new(
            id,
            resource.BoardId,
            resource.Project,
            resource.Title,
            resource.Description,
            resource.Assignee,
            resource.AssigneeAvatar,
            resource.AssigneeColor,
            resource.Status,
            resource.Priority,
            resource.PriorityColor,
            resource.Completed,
            resource.Comments,
            resource.Attachments,
            resource.DueDate,
            resource.Progress,
            resource.Department);
}
