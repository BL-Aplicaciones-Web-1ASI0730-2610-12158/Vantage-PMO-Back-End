using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.SystemAdministration.Application.CommandServices;
using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;
using vantagePMO_platform.SystemAdministration.Domain.Repositories;

namespace vantagePMO_platform.SystemAdministration.Application.Internal.CommandServices;

public class SystemAdministrationCommandService(
    IBrandingRepository brandingRepository,
    ISubscriptionRepository subscriptionRepository,
    IAdminPolicyRepository adminPolicyRepository,
    ISystemSettingsRepository systemSettingsRepository,
    IUnitOfWork unitOfWork) : ISystemAdministrationCommandService
{
    public async Task<Branding?> UpdateBrandingAsync(
        UpdateBrandingCommand command,
        CancellationToken cancellationToken = default)
    {
        var branding = await brandingRepository.GetSingletonAsync(cancellationToken);
        if (branding is null)
            return null;

        branding.Update(
            command.CompanyName,
            command.CompanyDescription,
            command.LogoUrl,
            command.PrimaryColor,
            command.SecondaryColor,
            command.TypographyStyle,
            command.UpdatedAt);

        brandingRepository.Update(branding);
        await unitOfWork.CompleteAsync(cancellationToken);
        return branding;
    }

    public async Task<Subscription?> UpdateSubscriptionAsync(
        UpdateSubscriptionCommand command,
        CancellationToken cancellationToken = default)
    {
        var subscription = await subscriptionRepository.GetSingletonAsync(cancellationToken);
        if (subscription is null)
            return null;

        subscription.Update(
            command.Plan,
            command.ActiveUsers,
            command.BillingCycle,
            command.ExpirationDate,
            command.Status,
            command.UpdatedAt);

        subscriptionRepository.Update(subscription);
        await unitOfWork.CompleteAsync(cancellationToken);
        return subscription;
    }

    public async Task<Subscription?> RenewSubscriptionAsync(
        int subscriptionId,
        CancellationToken cancellationToken = default)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(subscriptionId, cancellationToken);
        if (subscription is null)
            return null;

        subscription.Renew(DateTime.UtcNow.ToString("yyyy-MM-dd"));
        subscriptionRepository.Update(subscription);
        await unitOfWork.CompleteAsync(cancellationToken);
        return subscription;
    }

    public async Task<AdminPolicy?> UpdateAdminPolicyAsync(
        UpdateAdminPolicyCommand command,
        CancellationToken cancellationToken = default)
    {
        var policy = await adminPolicyRepository.GetSingletonAsync(cancellationToken);
        if (policy is null)
            return null;

        policy.Update(
            command.PasswordPolicy,
            command.MfaRequired,
            command.SessionTimeout,
            command.ApiRequestLimits,
            command.NotificationPermissions,
            command.JwtEnabled,
            command.EncryptedPasswords,
            command.ApiProtection,
            command.PasswordExpiration,
            command.AllowedDevices,
            command.IpRestriction,
            command.MinimumPasswordLength,
            command.RequireSymbols,
            command.RequireUppercase,
            command.UpdatedAt);

        adminPolicyRepository.Update(policy);
        await unitOfWork.CompleteAsync(cancellationToken);
        return policy;
    }

    public async Task<SystemSettings?> UpdateSystemSettingsAsync(
        UpdateSystemSettingsCommand command,
        CancellationToken cancellationToken = default)
    {
        var settings = await systemSettingsRepository.GetSingletonAsync(cancellationToken);
        if (settings is null)
            return null;

        settings.Update(
            command.EmailNotifications,
            command.PushNotifications,
            command.ReportAlerts,
            command.AdminAlerts,
            command.ProjectAlerts,
            command.WeeklyDigest,
            command.MentionNotifications,
            command.DeadlineReminders,
            command.BrowserPush,
            command.MobilePush,
            command.SoundAlerts,
            command.SecurityAlerts,
            command.WorkspaceUpdates,
            command.SubscriptionAlerts,
            command.TeamActivity,
            command.UpdatedAt);

        systemSettingsRepository.Update(settings);
        await unitOfWork.CompleteAsync(cancellationToken);
        return settings;
    }
}
