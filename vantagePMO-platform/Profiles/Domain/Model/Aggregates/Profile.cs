using vantagePMO_platform.Profiles.Domain.Model.Commands;
using vantagePMO_platform.Profiles.Domain.Model.ValueObjects;
using vantagePMO_platform.Shared.Domain.Model.Entities;

namespace vantagePMO_platform.Profiles.Domain.Model.Aggregates;

/// <summary>
///     Profile aggregate root for the Profiles bounded context.
/// </summary>
public class Profile : IAuditableEntity
{
    /// <summary>
    ///     Required by EF Core. Initializes empty value objects/collections so the
    ///     materializer can populate them through the configured conversions.
    /// </summary>
    protected Profile()
    {
        Name = null!;
        Email = null!;
        Role = string.Empty;
        Department = string.Empty;
        Joined = string.Empty;
        AvatarSeed = string.Empty;
        Availability = string.Empty;
        Experience = string.Empty;
        DeliveryRate = string.Empty;
        ActiveBudget = string.Empty;
        Bio = new List<string>();
        Certifications = new List<string>();
        SkillsDescription = string.Empty;
        Location = string.Empty;
        YearsActive = string.Empty;
        AvailabilityLabel = string.Empty;
        DateOfBirth = null;
    }

    /// <summary>
    ///     Creates a new profile from a <see cref="CreateProfileCommand" />.
    /// </summary>
    /// <param name="command">The validated creation command.</param>
    /// <exception cref="ArgumentException">Thrown when name/email value objects are invalid.</exception>
    public Profile(CreateProfileCommand command)
    {
        if (command.UserId <= 0)
            throw new ArgumentException("UserId must be greater than zero.", nameof(command));

        UserId = command.UserId;
        Name = new PersonName(command.Name);
        Email = new EmailAddress(command.Email);
        Role = command.Role ?? string.Empty;
        DateOfBirth = command.DateOfBirth;
        Department = command.Department ?? string.Empty;
        Joined = command.Joined ?? string.Empty;
        AvatarSeed = command.AvatarSeed ?? string.Empty;
        Availability = command.Availability ?? string.Empty;
        Experience = command.Experience ?? string.Empty;
        DeliveryRate = command.DeliveryRate ?? string.Empty;
        ActiveBudget = command.ActiveBudget ?? string.Empty;
        Bio = command.Bio?.ToList() ?? new List<string>();
        Certifications = command.Certifications?.ToList() ?? new List<string>();
        SkillsDescription = command.SkillsDescription ?? string.Empty;
        Location = command.Location ?? string.Empty;
        YearsActive = command.YearsActive ?? string.Empty;
        AvailabilityLabel = command.AvailabilityLabel ?? string.Empty;
    }

    /// <summary>Gets the profile identifier.</summary>
    public int Id { get; }

    /// <summary>Gets the IAM user identifier this profile belongs to.</summary>
    public int UserId { get; private set; }

    /// <summary>Gets the profile owner's full name.</summary>
    public PersonName Name { get; private set; }

    /// <summary>Gets the profile owner's email address (unique).</summary>
    public EmailAddress Email { get; private set; }

    /// <summary>Gets the professional role/title.</summary>
    public string Role { get; private set; }

    /// <summary>Gets the profile owner's date of birth.</summary>
    public DateOnly? DateOfBirth { get; private set; }

    /// <summary>Gets the department the owner belongs to.</summary>
    public string Department { get; private set; }

    /// <summary>Gets the textual joined date label (e.g. "January 2022").</summary>
    public string Joined { get; private set; }

    /// <summary>Gets the avatar seed used to render the avatar.</summary>
    public string AvatarSeed { get; private set; }

    /// <summary>Gets the short availability tag.</summary>
    public string Availability { get; private set; }

    /// <summary>Gets the years of experience label.</summary>
    public string Experience { get; private set; }

    /// <summary>Gets the delivery rate label.</summary>
    public string DeliveryRate { get; private set; }

    /// <summary>Gets the active budget label.</summary>
    public string ActiveBudget { get; private set; }

    /// <summary>Gets the multi-paragraph biography.</summary>
    public List<string> Bio { get; private set; }

    /// <summary>Gets the list of certifications.</summary>
    public List<string> Certifications { get; private set; }

    /// <summary>Gets the free-form skills description.</summary>
    public string SkillsDescription { get; private set; }

    /// <summary>Gets the location label.</summary>
    public string Location { get; private set; }

    /// <summary>Gets the years active label.</summary>
    public string YearsActive { get; private set; }

    /// <summary>Gets the availability label shown on the profile header.</summary>
    public string AvailabilityLabel { get; private set; }

    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    ///     Applies a partial update from an <see cref="UpdateProfileCommand" />.
    ///     Only non-null command fields are applied.
    /// </summary>
    /// <param name="command">The partial update command.</param>
    /// <exception cref="ArgumentException">Thrown when a provided name/email is invalid.</exception>
    public void Update(UpdateProfileCommand command)
    {
        if (command.Name is not null) Name = new PersonName(command.Name);
        if (command.Email is not null) Email = new EmailAddress(command.Email);
        if (command.Role is not null) Role = command.Role;
        if (command.DateOfBirth is not null) DateOfBirth = command.DateOfBirth;
        if (command.Department is not null) Department = command.Department;
        if (command.Joined is not null) Joined = command.Joined;
        if (command.AvatarSeed is not null) AvatarSeed = command.AvatarSeed;
        if (command.Availability is not null) Availability = command.Availability;
        if (command.Experience is not null) Experience = command.Experience;
        if (command.DeliveryRate is not null) DeliveryRate = command.DeliveryRate;
        if (command.ActiveBudget is not null) ActiveBudget = command.ActiveBudget;
        if (command.Bio is not null) Bio = command.Bio.ToList();
        if (command.Certifications is not null) Certifications = command.Certifications.ToList();
        if (command.SkillsDescription is not null) SkillsDescription = command.SkillsDescription;
        if (command.Location is not null) Location = command.Location;
        if (command.YearsActive is not null) YearsActive = command.YearsActive;
        if (command.AvailabilityLabel is not null) AvailabilityLabel = command.AvailabilityLabel;
    }
}
