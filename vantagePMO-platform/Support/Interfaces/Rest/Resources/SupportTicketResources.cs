namespace vantagePMO_platform.Support.Interfaces.Rest.Resources;

public record SupportTicketResource(
    int Id,
    string Title,
    string Description,
    string Category,
    string Priority,
    string Status,
    string CreatedAt,
    string Assignee);

public record CreateSupportTicketResource(
    string Title,
    string? Description,
    string Category,
    string Priority,
    string Status,
    string CreatedAt,
    string Assignee);

public record UpdateSupportTicketResource(
    string Title,
    string? Description,
    string Category,
    string Priority,
    string Status,
    string CreatedAt,
    string Assignee);
