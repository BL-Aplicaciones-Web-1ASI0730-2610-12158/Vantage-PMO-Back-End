namespace vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

public class AdminPolicy
{
    public const int SingletonId = 1;

    protected AdminPolicy()
    {
        PasswordPolicy = string.Empty;
        PasswordExpiration = string.Empty;
        CreatedAt = string.Empty;
        UpdatedAt = string.Empty;
    }

    public AdminPolicy(
        string passwordPolicy,
        bool mfaRequired,
        int sessionTimeout,
        int apiRequestLimits,
        bool notificationPermissions,
        bool jwtEnabled,
        bool encryptedPasswords,
        bool apiProtection,
        string? passwordExpiration,
        bool allowedDevices,
        bool ipRestriction,
        int minimumPasswordLength,
        bool requireSymbols,
        bool requireUppercase,
        string createdAt,
        string updatedAt)
    {
        Id = SingletonId;
        PasswordPolicy = passwordPolicy;
        MfaRequired = mfaRequired;
        SessionTimeout = sessionTimeout;
        ApiRequestLimits = apiRequestLimits;
        NotificationPermissions = notificationPermissions;
        JwtEnabled = jwtEnabled;
        EncryptedPasswords = encryptedPasswords;
        ApiProtection = apiProtection;
        PasswordExpiration = passwordExpiration ?? "90 days";
        AllowedDevices = allowedDevices;
        IpRestriction = ipRestriction;
        MinimumPasswordLength = minimumPasswordLength;
        RequireSymbols = requireSymbols;
        RequireUppercase = requireUppercase;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int Id { get; private set; }
    public string PasswordPolicy { get; private set; }
    public bool MfaRequired { get; private set; }
    public int SessionTimeout { get; private set; }
    public int ApiRequestLimits { get; private set; }
    public bool NotificationPermissions { get; private set; }
    public bool JwtEnabled { get; private set; }
    public bool EncryptedPasswords { get; private set; }
    public bool ApiProtection { get; private set; }
    public string PasswordExpiration { get; private set; }
    public bool AllowedDevices { get; private set; }
    public bool IpRestriction { get; private set; }
    public int MinimumPasswordLength { get; private set; }
    public bool RequireSymbols { get; private set; }
    public bool RequireUppercase { get; private set; }
    public string CreatedAt { get; private set; }
    public string UpdatedAt { get; private set; }

    public void Update(
        string passwordPolicy,
        bool mfaRequired,
        int sessionTimeout,
        int apiRequestLimits,
        bool notificationPermissions,
        bool jwtEnabled,
        bool encryptedPasswords,
        bool apiProtection,
        string? passwordExpiration,
        bool allowedDevices,
        bool ipRestriction,
        int minimumPasswordLength,
        bool requireSymbols,
        bool requireUppercase,
        string updatedAt)
    {
        PasswordPolicy = passwordPolicy;
        MfaRequired = mfaRequired;
        SessionTimeout = sessionTimeout;
        ApiRequestLimits = apiRequestLimits;
        NotificationPermissions = notificationPermissions;
        JwtEnabled = jwtEnabled;
        EncryptedPasswords = encryptedPasswords;
        ApiProtection = apiProtection;
        PasswordExpiration = passwordExpiration ?? PasswordExpiration;
        AllowedDevices = allowedDevices;
        IpRestriction = ipRestriction;
        MinimumPasswordLength = minimumPasswordLength;
        RequireSymbols = requireSymbols;
        RequireUppercase = requireUppercase;
        UpdatedAt = updatedAt;
    }
}
