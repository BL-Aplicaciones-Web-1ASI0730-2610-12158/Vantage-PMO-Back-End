using vantagePMO_platform.SystemAdministration.Application.CommandServices;
using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;
using vantagePMO_platform.SystemAdministration.Interfaces.Rest.Resources;

namespace vantagePMO_platform.SystemAdministration.Interfaces.Rest.Transform;

public static class SystemAdministrationResourceFromEntityAssembler
{
    public static BrandingResource ToResource(Branding branding) =>
        new(
            branding.Id,
            branding.CompanyName,
            branding.CompanyDescription,
            branding.LogoUrl,
            branding.PrimaryColor,
            branding.SecondaryColor,
            branding.TypographyStyle,
            branding.CreatedAt,
            branding.UpdatedAt);

    public static SubscriptionResource ToResource(Subscription subscription) =>
        new(
            subscription.Id,
            subscription.Plan,
            subscription.ActiveUsers,
            subscription.BillingCycle,
            subscription.ExpirationDate,
            subscription.Status,
            subscription.CreatedAt,
            subscription.UpdatedAt);

    public static AdminPolicyResource ToResource(AdminPolicy policy) =>
        new(
            policy.Id,
            policy.PasswordPolicy,
            policy.MfaRequired,
            policy.SessionTimeout,
            policy.ApiRequestLimits,
            policy.NotificationPermissions,
            policy.JwtEnabled,
            policy.EncryptedPasswords,
            policy.ApiProtection,
            policy.PasswordExpiration,
            policy.AllowedDevices,
            policy.IpRestriction,
            policy.MinimumPasswordLength,
            policy.RequireSymbols,
            policy.RequireUppercase,
            policy.CreatedAt,
            policy.UpdatedAt);

    public static SystemSettingsResource ToResource(SystemSettings settings) =>
        new(
            settings.Id,
            settings.EmailNotifications,
            settings.PushNotifications,
            settings.ReportAlerts,
            settings.AdminAlerts,
            settings.ProjectAlerts,
            settings.WeeklyDigest,
            settings.MentionNotifications,
            settings.DeadlineReminders,
            settings.BrowserPush,
            settings.MobilePush,
            settings.SoundAlerts,
            settings.SecurityAlerts,
            settings.WorkspaceUpdates,
            settings.SubscriptionAlerts,
            settings.TeamActivity,
            settings.CreatedAt,
            settings.UpdatedAt);

    public static LoginAttemptResource ToResource(LoginAttempt attempt) =>
        new(attempt.Id, attempt.User, attempt.Timestamp, attempt.Status);

    public static IEnumerable<LoginAttemptResource> ToResources(IEnumerable<LoginAttempt> attempts) =>
        attempts.Select(ToResource);
}

public static class SystemAdministrationCommandFromResourceAssembler
{
    private static string Today() => DateTime.UtcNow.ToString("yyyy-MM-dd");

    public static UpdateBrandingCommand ToCommand(UpdateBrandingResource resource) =>
        new(
            resource.CompanyName,
            resource.CompanyDescription,
            resource.LogoUrl ?? string.Empty,
            resource.PrimaryColor,
            resource.SecondaryColor,
            resource.TypographyStyle,
            Today());

    public static UpdateSubscriptionCommand ToCommand(UpdateSubscriptionResource resource) =>
        new(
            resource.Plan,
            resource.ActiveUsers,
            resource.BillingCycle,
            resource.ExpirationDate,
            resource.Status,
            Today());

    public static UpdateAdminPolicyCommand ToCommand(UpdateAdminPolicyResource resource, AdminPolicy current) =>
        new(
            resource.PasswordPolicy ?? current.PasswordPolicy,
            resource.MfaRequired ?? current.MfaRequired,
            resource.SessionTimeout ?? current.SessionTimeout,
            resource.ApiRequestLimits ?? current.ApiRequestLimits,
            resource.NotificationPermissions ?? current.NotificationPermissions,
            resource.JwtEnabled ?? current.JwtEnabled,
            resource.EncryptedPasswords ?? current.EncryptedPasswords,
            resource.ApiProtection ?? current.ApiProtection,
            resource.PasswordExpiration ?? current.PasswordExpiration,
            resource.AllowedDevices ?? current.AllowedDevices,
            resource.IpRestriction ?? current.IpRestriction,
            resource.MinimumPasswordLength ?? current.MinimumPasswordLength,
            resource.RequireSymbols ?? current.RequireSymbols,
            resource.RequireUppercase ?? current.RequireUppercase,
            Today());

    public static UpdateSystemSettingsCommand ToCommand(UpdateSystemSettingsResource resource, SystemSettings current) =>
        new(
            resource.EmailNotifications ?? current.EmailNotifications,
            resource.PushNotifications ?? current.PushNotifications,
            resource.ReportAlerts ?? current.ReportAlerts,
            resource.AdminAlerts ?? current.AdminAlerts,
            resource.ProjectAlerts ?? current.ProjectAlerts,
            resource.WeeklyDigest ?? current.WeeklyDigest,
            resource.MentionNotifications ?? current.MentionNotifications,
            resource.DeadlineReminders ?? current.DeadlineReminders,
            resource.BrowserPush ?? current.BrowserPush,
            resource.MobilePush ?? current.MobilePush,
            resource.SoundAlerts ?? current.SoundAlerts,
            resource.SecurityAlerts ?? current.SecurityAlerts,
            resource.WorkspaceUpdates ?? current.WorkspaceUpdates,
            resource.SubscriptionAlerts ?? current.SubscriptionAlerts,
            resource.TeamActivity ?? current.TeamActivity,
            Today());
}
