using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;
using vantagePMO_platform.ChatHub.Domain.Model.Queries;

namespace vantagePMO_platform.ChatHub.Application.QueryServices;

public interface IChatUserQueryService
{
    Task<IReadOnlyList<ChatUser>> Handle(GetAllChatUsersQuery query, CancellationToken cancellationToken = default);
}

public interface IChatQueryService
{
    Task<IReadOnlyList<Chat>> Handle(GetAllChatsQuery query, CancellationToken cancellationToken = default);
}

public interface IChatMessageQueryService
{
    Task<IReadOnlyList<ChatMessage>> Handle(GetAllChatMessagesQuery query, CancellationToken cancellationToken = default);
}

public interface IChatPinnedAssetQueryService
{
    Task<IReadOnlyList<ChatPinnedAsset>> Handle(
        GetChatPinnedAssetsByChatIdQuery query,
        CancellationToken cancellationToken = default);
}

public interface IChatInsightQueryService
{
    Task<IReadOnlyList<ChatInsight>> Handle(
        GetChatInsightsByChatIdQuery query,
        CancellationToken cancellationToken = default);
}
