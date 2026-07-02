using vantagePMO_platform.Support.Domain.Model.Commands;

namespace vantagePMO_platform.Support.Domain.Model.Aggregates;

/// <summary>
///     Support ticket aggregate for the Support bounded context.
/// </summary>
public class SupportTicket
{
    protected SupportTicket()
    {
        Title = string.Empty;
        Description = string.Empty;
        Category = string.Empty;
        Priority = string.Empty;
        Status = string.Empty;
        CreatedAt = string.Empty;
        Assignee = string.Empty;
    }

    public SupportTicket(CreateSupportTicketCommand command) : this()
    {
        Title = command.Title;
        Description = command.Description ?? string.Empty;
        Category = command.Category;
        Priority = command.Priority;
        Status = command.Status;
        CreatedAt = command.CreatedAt;
        Assignee = command.Assignee;
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public string Priority { get; private set; }
    public string Status { get; private set; }
    public string CreatedAt { get; private set; }
    public string Assignee { get; private set; }

    public void Update(UpdateSupportTicketCommand command)
    {
        Title = command.Title;
        Description = command.Description ?? string.Empty;
        Category = command.Category;
        Priority = command.Priority;
        Status = command.Status;
        CreatedAt = command.CreatedAt;
        Assignee = command.Assignee;
    }
}
