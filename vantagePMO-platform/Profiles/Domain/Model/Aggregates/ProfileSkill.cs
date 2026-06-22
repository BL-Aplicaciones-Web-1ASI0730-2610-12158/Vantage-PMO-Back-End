namespace vantagePMO_platform.Profiles.Domain.Model.Aggregates;

/// <summary>
///     A skill entry displayed on a user profile.
/// </summary>
public class ProfileSkill
{
    protected ProfileSkill()
    {
        Name = string.Empty;
    }

    public ProfileSkill(int userId, string name, int percentage)
    {
        UserId = userId;
        Name = name;
        Percentage = percentage;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public int Percentage { get; private set; }
}
