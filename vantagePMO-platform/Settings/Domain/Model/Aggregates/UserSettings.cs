using vantagePMO_platform.Settings.Domain.Model.Commands;

namespace vantagePMO_platform.Settings.Domain.Model.Aggregates;

/// <summary>
///     User settings aggregate (singleton per deployment).
/// </summary>
public class UserSettings
{
    public const int SingletonId = 1;

    protected UserSettings()
    {
        Theme = string.Empty;
        Language = string.Empty;
        Timezone = string.Empty;
        DateFormat = string.Empty;
        TimeFormat = string.Empty;
        FirstDayOfWeek = string.Empty;
        Currency = string.Empty;
        AccentColor = string.Empty;
        Density = string.Empty;
        ProfileVisibility = string.Empty;
        DisplayName = string.Empty;
        JobTitle = string.Empty;
        Bio = string.Empty;
        Phone = string.Empty;
        Department = string.Empty;
        UpdatedAt = string.Empty;
    }

    public UserSettings(CreateUserSettingsCommand command) : this()
    {
        Id = SingletonId;
        Apply(command);
    }

    public int Id { get; private set; }
    public string Theme { get; private set; }
    public string Language { get; private set; }
    public string Timezone { get; private set; }
    public string DateFormat { get; private set; }
    public string TimeFormat { get; private set; }
    public string FirstDayOfWeek { get; private set; }
    public string Currency { get; private set; }
    public string AccentColor { get; private set; }
    public string Density { get; private set; }
    public bool EmailNotifications { get; private set; }
    public bool PushNotifications { get; private set; }
    public bool WeeklyDigest { get; private set; }
    public bool MentionAlerts { get; private set; }
    public bool TwoFactorEnabled { get; private set; }
    public string ProfileVisibility { get; private set; }
    public string DisplayName { get; private set; }
    public string JobTitle { get; private set; }
    public string Bio { get; private set; }
    public string Phone { get; private set; }
    public string Department { get; private set; }
    public string UpdatedAt { get; private set; }

    public void Update(UpdateUserSettingsCommand command)
    {
        Apply(command);
        UpdatedAt = command.UpdatedAt;
    }

    private void Apply(CreateUserSettingsCommand command)
    {
        Theme = command.Theme;
        Language = command.Language;
        Timezone = command.Timezone;
        DateFormat = command.DateFormat;
        TimeFormat = command.TimeFormat;
        FirstDayOfWeek = command.FirstDayOfWeek;
        Currency = command.Currency;
        AccentColor = command.AccentColor;
        Density = command.Density;
        EmailNotifications = command.EmailNotifications;
        PushNotifications = command.PushNotifications;
        WeeklyDigest = command.WeeklyDigest;
        MentionAlerts = command.MentionAlerts;
        TwoFactorEnabled = command.TwoFactorEnabled;
        ProfileVisibility = command.ProfileVisibility;
        DisplayName = command.DisplayName;
        JobTitle = command.JobTitle;
        Bio = command.Bio ?? string.Empty;
        Phone = command.Phone ?? string.Empty;
        Department = command.Department;
        UpdatedAt = command.UpdatedAt;
    }

    private void Apply(UpdateUserSettingsCommand command)
    {
        Theme = command.Theme;
        Language = command.Language;
        Timezone = command.Timezone;
        DateFormat = command.DateFormat;
        TimeFormat = command.TimeFormat;
        FirstDayOfWeek = command.FirstDayOfWeek;
        Currency = command.Currency;
        AccentColor = command.AccentColor;
        Density = command.Density;
        EmailNotifications = command.EmailNotifications;
        PushNotifications = command.PushNotifications;
        WeeklyDigest = command.WeeklyDigest;
        MentionAlerts = command.MentionAlerts;
        TwoFactorEnabled = command.TwoFactorEnabled;
        ProfileVisibility = command.ProfileVisibility;
        DisplayName = command.DisplayName;
        JobTitle = command.JobTitle;
        Bio = command.Bio ?? string.Empty;
        Phone = command.Phone ?? string.Empty;
        Department = command.Department;
    }
}
