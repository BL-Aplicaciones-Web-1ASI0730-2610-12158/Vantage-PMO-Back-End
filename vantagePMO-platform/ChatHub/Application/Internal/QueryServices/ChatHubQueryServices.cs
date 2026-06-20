using vantagePMO_platform.ChatHub.Application.QueryServices;
using vantagePMO_platform.ChatHub.Domain.Model.Queries;
using vantagePMO_platform.ChatHub.Domain.Repositories;

namespace vantagePMO_platform.ChatHub.Application.Internal.QueryServices;

public class ChatUserQueryService(IChatUserRepository chatUserRepository) : IChatUserQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.ChatUser>> Handle(
        GetAllChatUsersQuery query,
        CancellationToken cancellationToken = default)
    {
        return await chatUserRepository.ListAllAsync(cancellationToken);
    }
}

public class ChatQueryService(IChatRepository chatRepository) : IChatQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.Chat>> Handle(
        GetAllChatsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await chatRepository.ListAllAsync(cancellationToken);
    }
}

public class ChatMessageQueryService(IChatMessageRepository chatMessageRepository) : IChatMessageQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.ChatMessage>> Handle(
        GetAllChatMessagesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await chatMessageRepository.ListAllAsync(cancellationToken);
    }
}

public class ChatPinnedAssetQueryService(IChatPinnedAssetRepository chatPinnedAssetRepository) : IChatPinnedAssetQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.ChatPinnedAsset>> Handle(
        GetChatPinnedAssetsByChatIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await chatPinnedAssetRepository.ListByChatIdAsync(query.ChatId, cancellationToken);
    }
}

public class ChatInsightQueryService(IChatInsightRepository chatInsightRepository) : IChatInsightQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.ChatInsight>> Handle(
        GetChatInsightsByChatIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await chatInsightRepository.ListByChatIdAsync(query.ChatId, cancellationToken);
    }
}
