using vantagePMO_platform.ChatHub.Domain.Model.Commands;

namespace vantagePMO_platform.ChatHub.Domain.Model.Aggregates;

/// <summary>
///     Channel or direct-message conversation.
/// </summary>
public class Chat
{
    protected Chat()
    {
        Id = string.Empty;
        Name = string.Empty;
        Type = string.Empty;
        Description = string.Empty;
        Members = new List<string>();
    }

    public Chat(CreateChatCommand command)
    {
        Id = command.Id.Trim();
        Name = command.Name.Trim();
        Type = command.Type.Trim();
        Description = command.Description?.Trim() ?? string.Empty;
        Members = command.Members?.ToList() ?? new List<string>();
        IsFavorited = command.IsFavorited;
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Description { get; private set; }
    public List<string> Members { get; private set; }
    public bool IsFavorited { get; private set; }

    public void Patch(PatchChatCommand command)
    {
        if (command.Name is not null)
            Name = command.Name.Trim();

        if (command.Description is not null)
            Description = command.Description.Trim();

        if (command.Members is not null)
            Members = command.Members.ToList();

        if (command.IsFavorited.HasValue)
            IsFavorited = command.IsFavorited.Value;
    }
}
