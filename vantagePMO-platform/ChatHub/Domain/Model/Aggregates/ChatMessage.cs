using vantagePMO_platform.ChatHub.Domain.Model.Commands;
using vantagePMO_platform.ChatHub.Domain.Model.ValueObjects;

namespace vantagePMO_platform.ChatHub.Domain.Model.Aggregates;

/// <summary>
///     Message posted in a chat channel or DM.
/// </summary>
public class ChatMessage
{
    protected ChatMessage()
    {
        Id = string.Empty;
        ChatId = string.Empty;
        AuthorId = string.Empty;
        Timestamp = string.Empty;
        Text = string.Empty;
        Attachments = new List<ChatAttachment>();
        Reactions = new List<ChatReaction>();
    }

    public ChatMessage(CreateChatMessageCommand command)
    {
        Id = command.Id.Trim();
        ChatId = command.ChatId.Trim();
        AuthorId = command.AuthorId.Trim();
        Timestamp = command.Timestamp?.Trim() ?? string.Empty;
        Text = command.Text?.Trim() ?? string.Empty;
        Attachments = command.Attachments?.ToList() ?? new List<ChatAttachment>();
        Reactions = command.Reactions?.ToList() ?? new List<ChatReaction>();
    }

    public string Id { get; private set; }
    public string ChatId { get; private set; }
    public string AuthorId { get; private set; }
    public string Timestamp { get; private set; }
    public string Text { get; private set; }
    public List<ChatAttachment> Attachments { get; private set; }
    public List<ChatReaction> Reactions { get; private set; }
}
