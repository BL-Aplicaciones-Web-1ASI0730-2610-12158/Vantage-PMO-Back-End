namespace vantagePMO_platform.Profiles.Interfaces.REST.Resources;

public record EndorsementResource(
    int Id,
    int UserId,
    string Quote,
    string AuthorName,
    string AuthorRole,
    string AuthorAvatarSeed);
