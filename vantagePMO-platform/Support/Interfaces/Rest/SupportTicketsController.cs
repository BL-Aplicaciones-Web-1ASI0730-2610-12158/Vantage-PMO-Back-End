using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Support.Application.CommandServices;
using vantagePMO_platform.Support.Application.QueryServices;
using vantagePMO_platform.Support.Domain.Model.Queries;
using vantagePMO_platform.Support.Interfaces.Rest.Resources;
using vantagePMO_platform.Support.Interfaces.Rest.Transform;

namespace vantagePMO_platform.Support.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/support-tickets")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Support ticket management")]
public class SupportTicketsController(
    ISupportTicketCommandService supportTicketCommandService,
    ISupportTicketQueryService supportTicketQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all support tickets", OperationId = "GetAllSupportTickets")]
    [SwaggerResponse(StatusCodes.Status200OK, "Support tickets found.", typeof(IEnumerable<SupportTicketResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tickets = await supportTicketQueryService.Handle(new GetAllSupportTicketsQuery(), cancellationToken);
        return Ok(SupportTicketResourceFromEntityAssembler.ToResourcesFromEntities(tickets));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a support ticket", OperationId = "CreateSupportTicket")]
    [SwaggerResponse(StatusCodes.Status201Created, "Support ticket created.", typeof(SupportTicketResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid support ticket data.")]
    public async Task<IActionResult> Create(
        [FromBody] CreateSupportTicketResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateSupportTicketCommandFromResourceAssembler.ToCommandFromResource(resource);
        var ticket = await supportTicketCommandService.CreateAsync(command, cancellationToken);

        if (ticket is null)
            return BadRequest("Could not create support ticket.");

        var createdResource = SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket);
        return CreatedAtAction(nameof(GetAll), new { id = ticket.Id }, createdResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update a support ticket", OperationId = "UpdateSupportTicket")]
    [SwaggerResponse(StatusCodes.Status200OK, "Support ticket updated.", typeof(SupportTicketResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid support ticket data.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Support ticket not found.")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateSupportTicketResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateSupportTicketCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var ticket = await supportTicketCommandService.UpdateAsync(command, cancellationToken);

        if (ticket is null)
            return NotFound($"Support ticket {id} was not found.");

        return Ok(SupportTicketResourceFromEntityAssembler.ToResourceFromEntity(ticket));
    }
}
