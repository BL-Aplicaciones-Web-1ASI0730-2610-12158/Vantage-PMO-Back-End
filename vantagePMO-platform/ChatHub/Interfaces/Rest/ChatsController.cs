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
using VantagePMO_platform.Shared.Application.Model;
using VantagePMO_platform.Shared.Interfaces.Rest.ProblemDetails;

namespace vantagePMO_platform.ChatHub.Interfaces.Rest;

[ApiController]
[Route("api/v1/chats")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Chat Hub conversations")]
public class ChatsController(
    IChatCommandService chatCommandService,
    IChatQueryService chatQueryService,
    ProblemDetailsFactory problemDetailsFactory,
    ILogger<ChatsController> logger) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all chats", OperationId = "GetAllChats")]
    [SwaggerResponse(StatusCodes.Status200OK, "Chats found.", typeof(IEnumerable<ChatResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var chats = await chatQueryService.Handle(new GetAllChatsQuery(), cancellationToken);
        return Ok(ChatResourceFromEntityAssembler.ToResourcesFromEntities(chats));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a chat", OperationId = "CreateChat")]
    [SwaggerResponse(StatusCodes.Status201Created, "Chat created.", typeof(ChatResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid chat data.")]
    public async Task<IActionResult> Create(
        [FromBody] CreateChatResource resource,
        CancellationToken cancellationToken)
    {
        var command = ChatCommandFromResourceAssembler.ToCreateCommandFromResource(resource);
        var result = await chatCommandService.CreateChat(command, cancellationToken);

        if (result.IsSuccess)
        {
            return CreatedAtAction(
                nameof(GetAll),
                ChatResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
        }

        return MapErrorToActionResult(result);
    }

    [HttpPatch("{id}")]
    [SwaggerOperation(Summary = "Partially update a chat", OperationId = "PatchChat")]
    [SwaggerResponse(StatusCodes.Status200OK, "Chat updated.", typeof(ChatResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Chat not found.")]
    public async Task<IActionResult> Patch(
        string id,
        [FromBody] PatchChatResource resource,
        CancellationToken cancellationToken)
    {
        var command = ChatCommandFromResourceAssembler.ToPatchCommandFromResource(id, resource);
        var result = await chatCommandService.PatchChat(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(ChatResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));

        return MapErrorToActionResult(result);
    }

    private IActionResult MapErrorToActionResult(Result<Chat> result)
    {
        var error = result.Error as ChatHubError?;
        var statusCode = error switch
        {
            ChatHubError.ChatNotFound => StatusCodes.Status404NotFound,
            ChatHubError.InvalidChatData => StatusCodes.Status400BadRequest,
            ChatHubError.OperationCancelled => StatusCodes.Status400BadRequest,
            ChatHubError.DatabaseError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            logger.LogError("Chat operation failed: {Message}", result.Message);

        return problemDetailsFactory.CreateProblemDetails(this, statusCode, result.Error, result.Message);
    }
}
