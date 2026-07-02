namespace vantagePMO_platform.TaskCollaboration.Domain.Model.Commands;

public record CreateCollaborationTaskCommand(
    int BoardId,
    string? Project,
    string Title,
    string? Description,
    string? Assignee,
    string? AssigneeAvatar,
    string? AssigneeColor,
    string Status,
    string Priority,
    string? PriorityColor,
    bool Completed,
    int Comments,
    int Attachments,
    string? DueDate,
    int Progress,
    string? Department);

public record UpdateCollaborationTaskCommand(
    int TaskId,
    int BoardId,
    string? Project,
    string Title,
    string? Description,
    string? Assignee,
    string? AssigneeAvatar,
    string? AssigneeColor,
    string Status,
    string Priority,
    string? PriorityColor,
    bool Completed,
    int Comments,
    int Attachments,
    string? DueDate,
    int Progress,
    string? Department);
