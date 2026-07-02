using vantagePMO_platform.Settings.Domain.Model.Aggregates;
using vantagePMO_platform.Settings.Domain.Model.Commands;
using vantagePMO_platform.Settings.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Settings.Interfaces.Rest.Transform;

public static class UserSettingsResourceFromEntityAssembler
{
    public static UserSettingsResource ToResourceFromEntity(UserSettings settings) =>
        new(
            settings.Id,
            settings.Theme,
            settings.Language,
            settings.Timezone,
            settings.DateFormat,
            settings.TimeFormat,
            settings.FirstDayOfWeek,
            settings.Currency,
            settings.AccentColor,
            settings.Density,
            settings.EmailNotifications,
            settings.PushNotifications,
            settings.WeeklyDigest,
            settings.MentionAlerts,
            settings.TwoFactorEnabled,
            settings.ProfileVisibility,
            settings.DisplayName,
            settings.JobTitle,
            settings.Bio,
            settings.Phone,
            settings.Department,
            settings.UpdatedAt);
}

public static class UpdateUserSettingsCommandFromResourceAssembler
{
    public static UpdateUserSettingsCommand ToCommandFromResource(UpdateUserSettingsResource resource) =>
        new(
            resource.Theme,
            resource.Language,
            resource.Timezone,
            resource.DateFormat,
            resource.TimeFormat,
            resource.FirstDayOfWeek,
            resource.Currency,
            resource.AccentColor,
            resource.Density,
            resource.EmailNotifications,
            resource.PushNotifications,
            resource.WeeklyDigest,
            resource.MentionAlerts,
            resource.TwoFactorEnabled,
            resource.ProfileVisibility,
            resource.DisplayName,
            resource.JobTitle,
            resource.Bio,
            resource.Phone,
            resource.Department,
            DateTime.UtcNow.ToString("yyyy-MM-dd"));
}
