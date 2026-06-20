namespace vantagePMO_platform.ChatHub.Domain.Model.Queries;

public record GetAllChatUsersQuery;

public record GetAllChatsQuery;

public record GetAllChatMessagesQuery;

public record GetChatPinnedAssetsByChatIdQuery(string ChatId);

public record GetChatInsightsByChatIdQuery(string ChatId);
