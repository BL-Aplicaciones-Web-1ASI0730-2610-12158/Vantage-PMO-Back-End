namespace vantagePMO_platform.SystemAdministration.Application.CommandServices;

public record UpdateBrandingCommand(
    string CompanyName,
    string? CompanyDescription,
    string LogoUrl,
    string PrimaryColor,
    string SecondaryColor,
    string? TypographyStyle,
    string UpdatedAt);

public record UpdateSubscriptionCommand(
    string Plan,
    int ActiveUsers,
    string BillingCycle,
    string ExpirationDate,
    string Status,
    string UpdatedAt);

public record UpdateAdminPolicyCommand(
    string PasswordPolicy,
    bool MfaRequired,
    int SessionTimeout,
    int ApiRequestLimits,
    bool NotificationPermissions,
    bool JwtEnabled,
    bool EncryptedPasswords,
    bool ApiProtection,
    string? PasswordExpiration,
    bool AllowedDevices,
    bool IpRestriction,
    int MinimumPasswordLength,
    bool RequireSymbols,
    bool RequireUppercase,
    string UpdatedAt);

public record UpdateSystemSettingsCommand(
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
    string UpdatedAt);
