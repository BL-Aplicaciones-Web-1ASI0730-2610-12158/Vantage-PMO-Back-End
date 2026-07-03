namespace vantagePMO_platform.Settings.Domain.Model.Commands;

public record CreateUserSettingsCommand(
    string Theme,
    string Language,
    string Timezone,
    string DateFormat,
    string TimeFormat,
    string FirstDayOfWeek,
    string Currency,
    string AccentColor,
    string Density,
    bool EmailNotifications,
    bool PushNotifications,
    bool WeeklyDigest,
    bool MentionAlerts,
    bool TwoFactorEnabled,
    string ProfileVisibility,
    string DisplayName,
    string JobTitle,
    string? Bio,
    string? Phone,
    string Department,
    string UpdatedAt);
