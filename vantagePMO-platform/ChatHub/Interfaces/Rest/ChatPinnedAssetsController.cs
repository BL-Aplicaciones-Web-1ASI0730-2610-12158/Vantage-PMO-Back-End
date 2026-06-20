using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.ChatHub.Application.QueryServices;
using vantagePMO_platform.ChatHub.Domain.Model.Queries;
using vantagePMO_platform.ChatHub.Interfaces.Rest.Resources;
using vantagePMO_platform.ChatHub.Interfaces.Rest.Transform;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace vantagePMO_platform.ChatHub.Interfaces.Rest;

[AllowAnonymous]
[ApiController]
[Route("api/v1/chat-pinned-assets")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Chat Hub pinned assets")]
public class ChatPinnedAssetsController(IChatPinnedAssetQueryService chatPinnedAssetQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get pinned assets for a chat", OperationId = "GetChatPinnedAssets")]
    [SwaggerResponse(StatusCodes.Status200OK, "Pinned assets found.", typeof(IEnumerable<ChatPinnedAssetResource>))]
    public async Task<IActionResult> GetAll([FromQuery] string chatId, CancellationToken cancellationToken)
    {
        var assets = await chatPinnedAssetQueryService.Handle(
            new GetChatPinnedAssetsByChatIdQuery(chatId),
            cancellationToken);
        return Ok(ChatPinnedAssetResourceFromEntityAssembler.ToResourcesFromEntities(assets));
    }
}
