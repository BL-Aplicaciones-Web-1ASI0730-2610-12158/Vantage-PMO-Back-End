namespace vantagePMO_platform.ChatHub.Domain.Model.Commands;

public record CreateChatCommand(
    string Id,
    string Name,
    string Type,
    string? Description,
    IEnumerable<string>? Members,
    bool IsFavorited);
