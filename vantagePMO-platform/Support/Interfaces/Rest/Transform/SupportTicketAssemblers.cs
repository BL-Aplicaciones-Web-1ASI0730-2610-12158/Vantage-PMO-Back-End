using vantagePMO_platform.Support.Domain.Model.Aggregates;
using vantagePMO_platform.Support.Domain.Model.Commands;
using vantagePMO_platform.Support.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Support.Interfaces.Rest.Transform;

public static class SupportTicketResourceFromEntityAssembler
{
    public static SupportTicketResource ToResourceFromEntity(SupportTicket ticket) =>
        new(
            ticket.Id,
            ticket.Title,
            ticket.Description,
            ticket.Category,
            ticket.Priority,
            ticket.Status,
            ticket.CreatedAt,
            ticket.Assignee);

    public static IEnumerable<SupportTicketResource> ToResourcesFromEntities(IEnumerable<SupportTicket> tickets) =>
        tickets.Select(ToResourceFromEntity);
}

public static class CreateSupportTicketCommandFromResourceAssembler
{
    public static CreateSupportTicketCommand ToCommandFromResource(CreateSupportTicketResource resource) =>
        new(
            resource.Title,
            resource.Description,
            resource.Category,
            resource.Priority,
            resource.Status,
            resource.CreatedAt,
            resource.Assignee);
}

public static class UpdateSupportTicketCommandFromResourceAssembler
{
    public static UpdateSupportTicketCommand ToCommandFromResource(int id, UpdateSupportTicketResource resource) =>
        new(
            id,
            resource.Title,
            resource.Description,
            resource.Category,
            resource.Priority,
            resource.Status,
            resource.CreatedAt,
            resource.Assignee);
}
