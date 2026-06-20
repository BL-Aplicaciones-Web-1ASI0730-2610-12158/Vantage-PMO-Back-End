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
[Route("api/v1/chat-users")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Chat Hub users")]
public class ChatUsersController(IChatUserQueryService chatUserQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all chat users", OperationId = "GetAllChatUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "Chat users found.", typeof(IEnumerable<ChatUserResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var users = await chatUserQueryService.Handle(new GetAllChatUsersQuery(), cancellationToken);
        return Ok(ChatUserResourceFromEntityAssembler.ToResourcesFromEntities(users));
    }
}
