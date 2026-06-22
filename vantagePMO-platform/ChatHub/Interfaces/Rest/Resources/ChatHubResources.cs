namespace vantagePMO_platform.ChatHub.Interfaces.Rest.Resources;

public record ChatUserResource(
    string Id,
    string Name,
    string AvatarSeed,
    string? Avatar,
    string Status,
    string Role);

public record ChatResource(
    string Id,
    string Name,
    string Type,
    string Description,
    IEnumerable<string> Members,
    bool IsFavorited);

public record ChatAttachmentResource(string Name, string Icon, string Type);

public record ChatReactionResource(string Emoji, int Count);

public record ChatMessageResource(
    string Id,
    string ChatId,
    string AuthorId,
    string Timestamp,
    string Text,
    IEnumerable<ChatAttachmentResource> Attachments,
    IEnumerable<ChatReactionResource> Reactions);

public record CreateChatResource(
    string Id,
    string Name,
    string Type,
    string? Description,
    IEnumerable<string>? Members,
    bool IsFavorited);

public record PatchChatResource(
    string? Name,
    string? Description,
    IEnumerable<string>? Members,
    bool? IsFavorited);

public record CreateChatMessageResource(
    string Id,
    string ChatId,
    string AuthorId,
    string? Timestamp,
    string? Text,
    IEnumerable<ChatAttachmentResource>? Attachments,
    IEnumerable<ChatReactionResource>? Reactions);

public record ChatPinnedAssetResource(
    int Id,
    string ChatId,
    string Name,
    string Type,
    string Meta);

public record InsightItemResource(int Id, string Type, string Text);

public record ChatInsightResource(
    string ChatId,
    string MeetingTag,
    string TimeAgo,
    string MeetingTitle,
    IEnumerable<InsightItemResource> Insights,
    int SentimentProductive,
    string SentimentText);
