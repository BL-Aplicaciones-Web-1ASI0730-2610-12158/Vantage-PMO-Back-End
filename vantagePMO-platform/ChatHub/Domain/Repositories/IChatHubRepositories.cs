using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;

namespace vantagePMO_platform.ChatHub.Domain.Repositories;

public interface IChatUserRepository
{
    Task<IReadOnlyList<ChatUser>> ListAllAsync(CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ChatUser> users, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

public interface IChatRepository
{
    Task<IReadOnlyList<Chat>> ListAllAsync(CancellationToken cancellationToken = default);
    Task<Chat?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
    Task AddAsync(Chat chat, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Chat> chats, CancellationToken cancellationToken = default);
    void Update(Chat chat);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

public interface IChatMessageRepository
{
    Task<IReadOnlyList<ChatMessage>> ListAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(ChatMessage message, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ChatMessage> messages, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

public interface IChatPinnedAssetRepository
{
    Task<IReadOnlyList<ChatPinnedAsset>> ListByChatIdAsync(string chatId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ChatPinnedAsset> assets, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

public interface IChatInsightRepository
{
    Task<IReadOnlyList<ChatInsight>> ListByChatIdAsync(string chatId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ChatInsight> insights, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
