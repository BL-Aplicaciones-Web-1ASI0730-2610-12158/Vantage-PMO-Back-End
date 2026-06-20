namespace vantagePMO_platform.ChatHub.Domain.Model.Aggregates;

/// <summary>
///     File pinned to a chat channel.
/// </summary>
public class ChatPinnedAsset
{
    protected ChatPinnedAsset()
    {
        ChatId = string.Empty;
        Name = string.Empty;
        Type = string.Empty;
        Meta = string.Empty;
    }

    public ChatPinnedAsset(int id, string chatId, string name, string type, string meta)
    {
        Id = id;
        ChatId = chatId;
        Name = name;
        Type = type;
        Meta = meta;
    }

    public ChatPinnedAsset(string chatId, string name, string type, string meta)
    {
        ChatId = chatId;
        Name = name;
        Type = type;
        Meta = meta;
    }

    public int Id { get; private set; }
    public string ChatId { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Meta { get; private set; }
}
