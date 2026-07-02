namespace vantagePMO_platform.SystemAdministration.Interfaces.Rest.Resources;

public record BrandingResource(
    int Id,
    string CompanyName,
    string CompanyDescription,
    string LogoUrl,
    string PrimaryColor,
    string SecondaryColor,
    string TypographyStyle,
    string CreatedAt,
    string UpdatedAt);

public record UpdateBrandingResource(
    int? Id,
    string CompanyName,
    string? CompanyDescription,
    string? LogoUrl,
    string PrimaryColor,
    string SecondaryColor,
    string? TypographyStyle,
    string? CreatedAt,
    string? UpdatedAt);

public record SubscriptionResource(
    int Id,
    string Plan,
    int ActiveUsers,
    string BillingCycle,
    string ExpirationDate,
    string Status,
    string CreatedAt,
    string UpdatedAt);

public record UpdateSubscriptionResource(
    int? Id,
    string Plan,
    int ActiveUsers,
    string BillingCycle,
    string ExpirationDate,
    string Status,
    string? CreatedAt,
    string? UpdatedAt);

public record AdminPolicyResource(
    int Id,
    string PasswordPolicy,
    bool MfaRequired,
    int SessionTimeout,
    int ApiRequestLimits,
    bool NotificationPermissions,
    bool JwtEnabled,
    bool EncryptedPasswords,
    bool ApiProtection,
    string PasswordExpiration,
    bool AllowedDevices,
    bool IpRestriction,
    int MinimumPasswordLength,
    bool RequireSymbols,
    bool RequireUppercase,
    string CreatedAt,
    string UpdatedAt);

public record UpdateAdminPolicyResource(
    int? Id,
    string? PasswordPolicy,
    bool? MfaRequired,
    int? SessionTimeout,
    int? ApiRequestLimits,
    bool? NotificationPermissions,
    bool? JwtEnabled,
    bool? EncryptedPasswords,
    bool? ApiProtection,
    string? PasswordExpiration,
    bool? AllowedDevices,
    bool? IpRestriction,
    int? MinimumPasswordLength,
    bool? RequireSymbols,
    bool? RequireUppercase,
    string? CreatedAt,
    string? UpdatedAt);

public record SystemSettingsResource(
    int Id,
    bool EmailNotifications,
    bool PushNotifications,
    bool ReportAlerts,
    bool AdminAlerts,
    bool ProjectAlerts,
    bool WeeklyDigest,
    bool MentionNotifications,
    bool DeadlineReminders,
    bool BrowserPush,
    bool MobilePush,
    bool SoundAlerts,
    bool SecurityAlerts,
    bool WorkspaceUpdates,
    bool SubscriptionAlerts,
    bool TeamActivity,
    string CreatedAt,
    string UpdatedAt);

public record UpdateSystemSettingsResource(
    int? Id,
    bool? EmailNotifications,
    bool? PushNotifications,
    bool? ReportAlerts,
    bool? AdminAlerts,
    bool? ProjectAlerts,
    bool? WeeklyDigest,
    bool? MentionNotifications,
    bool? DeadlineReminders,
    bool? BrowserPush,
    bool? MobilePush,
    bool? SoundAlerts,
    bool? SecurityAlerts,
    bool? WorkspaceUpdates,
    bool? SubscriptionAlerts,
    bool? TeamActivity,
    string? CreatedAt,
    string? UpdatedAt);

public record LoginAttemptResource(int Id, string User, string Timestamp, string Status);
