namespace vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;

public class BoardMember
{
    protected BoardMember()
    {
        Name = string.Empty;
        Role = string.Empty;
        Avatar = string.Empty;
        Color = string.Empty;
        Status = string.Empty;
    }

    public BoardMember(int boardId, string name, string role, string avatar, string color, string status)
    {
        BoardId = boardId;
        Name = name;
        Role = role;
        Avatar = avatar;
        Color = color;
        Status = status;
    }

    public int Id { get; private set; }
    public int BoardId { get; private set; }
    public string Name { get; private set; }
    public string Role { get; private set; }
    public string Avatar { get; private set; }
    public string Color { get; private set; }
    public string Status { get; private set; }
}
