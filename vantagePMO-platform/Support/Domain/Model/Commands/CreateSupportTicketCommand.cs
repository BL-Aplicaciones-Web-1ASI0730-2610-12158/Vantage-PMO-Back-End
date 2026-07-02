namespace vantagePMO_platform.Support.Domain.Model.Commands;

public record CreateSupportTicketCommand(
    string Title,
    string? Description,
    string Category,
    string Priority,
    string Status,
    string CreatedAt,
    string Assignee);
