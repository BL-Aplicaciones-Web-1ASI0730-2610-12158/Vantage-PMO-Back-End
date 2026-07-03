using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.ChatHub.Application.CommandServices;
using vantagePMO_platform.ChatHub.Application.QueryServices;
using vantagePMO_platform.ChatHub.Domain.Model;
using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;
using vantagePMO_platform.ChatHub.Domain.Model.Queries;
using vantagePMO_platform.ChatHub.Interfaces.Rest.Resources;
using vantagePMO_platform.ChatHub.Interfaces.Rest.Transform;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Interfaces.Rest.ProblemDetails;

namespace vantagePMO_platform.ChatHub.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/chat-messages")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Chat Hub messages")]
public class ChatMessagesController(
    IChatMessageCommandService chatMessageCommandService,
    IChatMessageQueryService chatMessageQueryService,
    ProblemDetailsFactory problemDetailsFactory,
    ILogger<ChatMessagesController> logger) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all chat messages", OperationId = "GetAllChatMessages")]
    [SwaggerResponse(StatusCodes.Status200OK, "Messages found.", typeof(IEnumerable<ChatMessageResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var messages = await chatMessageQueryService.Handle(new GetAllChatMessagesQuery(), cancellationToken);
        return Ok(ChatMessageResourceFromEntityAssembler.ToResourcesFromEntities(messages));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a chat message", OperationId = "CreateChatMessage")]
    [SwaggerResponse(StatusCodes.Status201Created, "Message created.", typeof(ChatMessageResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid message data.")]
    public async Task<IActionResult> Create(
        [FromBody] CreateChatMessageResource resource,
        CancellationToken cancellationToken)
    {
        var command = ChatMessageCommandFromResourceAssembler.ToCreateCommandFromResource(resource);
        var result = await chatMessageCommandService.CreateMessage(command, cancellationToken);

        if (result.IsSuccess)
        {
            return CreatedAtAction(
                nameof(GetAll),
                ChatMessageResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
        }

        return MapErrorToActionResult(result);
    }

    private IActionResult MapErrorToActionResult(Result<ChatMessage> result)
    {
        var error = result.Error as ChatHubError?;
        var statusCode = error switch
        {
            ChatHubError.InvalidMessageData => StatusCodes.Status400BadRequest,
            ChatHubError.OperationCancelled => StatusCodes.Status400BadRequest,
            ChatHubError.DatabaseError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            logger.LogError("Chat message operation failed: {Message}", result.Message);

        return problemDetailsFactory.CreateProblemDetails(this, statusCode, result.Error, result.Message);
    }
}
