using vantagePMO_platform.TaskCollaboration.Domain.Model.Commands;

namespace vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;

public class CollaborationTask
{
    protected CollaborationTask()
    {
        Project = string.Empty;
        Title = string.Empty;
        Description = string.Empty;
        Assignee = string.Empty;
        AssigneeAvatar = string.Empty;
        AssigneeColor = string.Empty;
        Status = string.Empty;
        Priority = string.Empty;
        PriorityColor = string.Empty;
        DueDate = string.Empty;
        Department = string.Empty;
    }

    public CollaborationTask(CreateCollaborationTaskCommand command) : this()
    {
        BoardId = command.BoardId;
        Apply(command);
    }

    public int Id { get; private set; }
    public int BoardId { get; private set; }
    public string Project { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Assignee { get; private set; }
    public string AssigneeAvatar { get; private set; }
    public string AssigneeColor { get; private set; }
    public string Status { get; private set; }
    public string Priority { get; private set; }
    public string PriorityColor { get; private set; }
    public bool Completed { get; private set; }
    public int Comments { get; private set; }
    public int Attachments { get; private set; }
    public string DueDate { get; private set; }
    public int Progress { get; private set; }
    public string Department { get; private set; }

    public void Update(UpdateCollaborationTaskCommand command)
    {
        BoardId = command.BoardId;
        Apply(command);
    }

    private void Apply(CreateCollaborationTaskCommand command)
    {
        Project = command.Project ?? string.Empty;
        Title = command.Title;
        Description = command.Description ?? string.Empty;
        Assignee = command.Assignee ?? string.Empty;
        AssigneeAvatar = command.AssigneeAvatar ?? string.Empty;
        AssigneeColor = command.AssigneeColor ?? "#6b7280";
        Status = command.Status;
        Priority = command.Priority;
        PriorityColor = command.PriorityColor ?? "#6b7280";
        Completed = command.Completed;
        Comments = command.Comments;
        Attachments = command.Attachments;
        DueDate = command.DueDate ?? string.Empty;
        Progress = command.Progress;
        Department = command.Department ?? string.Empty;
    }

    private void Apply(UpdateCollaborationTaskCommand command)
    {
        Project = command.Project ?? string.Empty;
        Title = command.Title;
        Description = command.Description ?? string.Empty;
        Assignee = command.Assignee ?? string.Empty;
        AssigneeAvatar = command.AssigneeAvatar ?? string.Empty;
        AssigneeColor = command.AssigneeColor ?? "#6b7280";
        Status = command.Status;
        Priority = command.Priority;
        PriorityColor = command.PriorityColor ?? "#6b7280";
        Completed = command.Completed;
        Comments = command.Comments;
        Attachments = command.Attachments;
        DueDate = command.DueDate ?? string.Empty;
        Progress = command.Progress;
        Department = command.Department ?? string.Empty;
    }
}
