using System.Text.Json.Serialization;

namespace vantagePMO_platform.Iam.Domain.Model.Aggregates;

/**
 * <summary>
 *     The user aggregate
 * </summary>
 * <remarks>
 *     This class is used to represent a user
 * </remarks>
 */
public partial class User
{
    protected User()
    {
        Username = string.Empty;
        PasswordHash = string.Empty;
    }

    public User(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    public int Id { get; private set; }
    public string Username { get; private set; }

    [JsonIgnore] public string PasswordHash { get; private set; }

    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}
