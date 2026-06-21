using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;
using vantagePMO_platform.ChatHub.Domain.Model.Commands;

namespace vantagePMO_platform.ChatHub.Application.CommandServices;

public interface IChatCommandService
{
    Task<VantagePMO_platform.Shared.Application.Model.Result<Chat>> CreateChat(
        CreateChatCommand command,
        CancellationToken cancellationToken = default);

    Task<VantagePMO_platform.Shared.Application.Model.Result<Chat>> PatchChat(
        PatchChatCommand command,
        CancellationToken cancellationToken = default);
}

public interface IChatMessageCommandService
{
    Task<VantagePMO_platform.Shared.Application.Model.Result<ChatMessage>> CreateMessage(
        CreateChatMessageCommand command,
        CancellationToken cancellationToken = default);
}
