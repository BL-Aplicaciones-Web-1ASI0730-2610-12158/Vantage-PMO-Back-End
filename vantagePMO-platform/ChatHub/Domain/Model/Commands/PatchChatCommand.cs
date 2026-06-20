namespace vantagePMO_platform.ChatHub.Domain.Model.Commands;

public record PatchChatCommand(
    string Id,
    string? Name,
    string? Description,
    IEnumerable<string>? Members,
    bool? IsFavorited);
