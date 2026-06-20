namespace vantagePMO_platform.ChatHub.Domain.Model.ValueObjects;

public class ChatReaction
{
    public string Emoji { get; set; } = string.Empty;
    public int Count { get; set; }
}
