using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using vantagePMO_platform.ChatHub.Application.QueryServices;
using vantagePMO_platform.ChatHub.Domain.Model.Queries;
using vantagePMO_platform.ChatHub.Interfaces.Rest.Resources;
using vantagePMO_platform.ChatHub.Interfaces.Rest.Transform;

namespace vantagePMO_platform.ChatHub.Interfaces.Rest;

[ApiController]
[Route("api/v1/chat-insights")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Chat Hub AI insights")]
public class ChatInsightsController(IChatInsightQueryService chatInsightQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get AI insights for a chat", OperationId = "GetChatInsights")]
    [SwaggerResponse(StatusCodes.Status200OK, "Insights found.", typeof(IEnumerable<ChatInsightResource>))]
    public async Task<IActionResult> GetAll([FromQuery] string chatId, CancellationToken cancellationToken)
    {
        var insights = await chatInsightQueryService.Handle(
            new GetChatInsightsByChatIdQuery(chatId),
            cancellationToken);
        return Ok(ChatInsightResourceFromEntityAssembler.ToResourcesFromEntities(insights));
    }
}
