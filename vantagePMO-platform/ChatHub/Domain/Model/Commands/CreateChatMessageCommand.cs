using vantagePMO_platform.ChatHub.Domain.Model.ValueObjects;

namespace vantagePMO_platform.ChatHub.Domain.Model.Commands;

public record CreateChatMessageCommand(
    string Id,
    string ChatId,
    string AuthorId,
    string? Timestamp,
    string? Text,
    IEnumerable<ChatAttachment>? Attachments,
    IEnumerable<ChatReaction>? Reactions);
