using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;
using vantagePMO_platform.ChatHub.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.ChatHub.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ChatUserRepository(AppDbContext context) : IChatUserRepository
{
    public async Task<IReadOnlyList<ChatUser>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ChatUser>()
            .OrderBy(user => user.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ChatUser> users, CancellationToken cancellationToken = default)
    {
        await context.Set<ChatUser>().AddRangeAsync(users, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ChatUser>().AnyAsync(cancellationToken);
    }
}

public class ChatRepository(AppDbContext context) : IChatRepository
{
    public async Task<IReadOnlyList<Chat>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Chat>()
            .OrderBy(chat => chat.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Chat?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.Set<Chat>().FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(Chat chat, CancellationToken cancellationToken = default)
    {
        await context.Set<Chat>().AddAsync(chat, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Chat> chats, CancellationToken cancellationToken = default)
    {
        await context.Set<Chat>().AddRangeAsync(chats, cancellationToken);
    }

    public void Update(Chat chat)
    {
        context.Set<Chat>().Update(chat);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Chat>().AnyAsync(cancellationToken);
    }
}

public class ChatMessageRepository(AppDbContext context) : IChatMessageRepository
{
    public async Task<IReadOnlyList<ChatMessage>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ChatMessage>()
            .OrderBy(message => message.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ChatMessage message, CancellationToken cancellationToken = default)
    {
        await context.Set<ChatMessage>().AddAsync(message, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ChatMessage> messages, CancellationToken cancellationToken = default)
    {
        await context.Set<ChatMessage>().AddRangeAsync(messages, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ChatMessage>().AnyAsync(cancellationToken);
    }
}

public class ChatPinnedAssetRepository(AppDbContext context) : IChatPinnedAssetRepository
{
    public async Task<IReadOnlyList<ChatPinnedAsset>> ListByChatIdAsync(
        string chatId,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<ChatPinnedAsset>()
            .Where(asset => asset.ChatId == chatId)
            .OrderBy(asset => asset.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ChatPinnedAsset> assets, CancellationToken cancellationToken = default)
    {
        await context.Set<ChatPinnedAsset>().AddRangeAsync(assets, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ChatPinnedAsset>().AnyAsync(cancellationToken);
    }
}

public class ChatInsightRepository(AppDbContext context) : IChatInsightRepository
{
    public async Task<IReadOnlyList<ChatInsight>> ListByChatIdAsync(
        string chatId,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<ChatInsight>()
            .Where(insight => insight.ChatId == chatId)
            .OrderBy(insight => insight.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ChatInsight> insights, CancellationToken cancellationToken = default)
    {
        await context.Set<ChatInsight>().AddRangeAsync(insights, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<ChatInsight>().AnyAsync(cancellationToken);
    }
}
