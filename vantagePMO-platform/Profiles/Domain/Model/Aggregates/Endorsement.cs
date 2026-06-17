namespace vantagePMO_platform.Profiles.Domain.Model.Aggregates;

/// <summary>
///     A peer endorsement displayed on a user profile.
/// </summary>
public class Endorsement
{
    protected Endorsement()
    {
        Quote = string.Empty;
        AuthorName = string.Empty;
        AuthorRole = string.Empty;
        AuthorAvatarSeed = string.Empty;
    }

    public Endorsement(
        int userId,
        string quote,
        string authorName,
        string authorRole,
        string authorAvatarSeed)
    {
        UserId = userId;
        Quote = quote;
        AuthorName = authorName;
        AuthorRole = authorRole;
        AuthorAvatarSeed = authorAvatarSeed;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Quote { get; private set; }
    public string AuthorName { get; private set; }
    public string AuthorRole { get; private set; }
    public string AuthorAvatarSeed { get; private set; }
}
