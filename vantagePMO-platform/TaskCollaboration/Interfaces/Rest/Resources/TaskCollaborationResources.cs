namespace vantagePMO_platform.TaskCollaboration.Interfaces.Rest.Resources;

public record BoardResource(int Id, string Name, int ProjectsActive, string Description);

public record BoardMemberResource(
    int Id,
    int BoardId,
    string Name,
    string Role,
    string Avatar,
    string Color,
    string Status);

public record CollaborationTaskResource(
    int Id,
    int BoardId,
    string Project,
    string Title,
    string Description,
    string Assignee,
    string AssigneeAvatar,
    string AssigneeColor,
    string Status,
    string Priority,
    string PriorityColor,
    bool Completed,
    int Comments,
    int Attachments,
    string DueDate,
    int Progress,
    string Department);

public record CreateCollaborationTaskResource(
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

public record UpdateCollaborationTaskResource(
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
