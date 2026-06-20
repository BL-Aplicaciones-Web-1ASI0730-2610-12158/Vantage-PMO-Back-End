namespace vantagePMO_platform.ChatHub.Domain.Model.Aggregates;

/// <summary>
///     Chat user shown in the Chat Hub sidebar.
/// </summary>
public class ChatUser
{
    protected ChatUser()
    {
        Id = string.Empty;
        Name = string.Empty;
        AvatarSeed = string.Empty;
        Status = string.Empty;
        Role = string.Empty;
    }

    public ChatUser(string id, string name, string avatarSeed, string? avatar, string status, string role)
    {
        Id = id;
        Name = name;
        AvatarSeed = avatarSeed;
        Avatar = avatar;
        Status = status;
        Role = role;
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public string AvatarSeed { get; private set; }
    public string? Avatar { get; private set; }
    public string Status { get; private set; }
    public string Role { get; private set; }
}
