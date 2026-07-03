using vantagePMO_platform.SystemAdministration.Application.QueryServices;
using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;
using vantagePMO_platform.SystemAdministration.Domain.Repositories;

namespace vantagePMO_platform.SystemAdministration.Application.Internal.QueryServices;

public class SystemAdministrationQueryService(
    IBrandingRepository brandingRepository,
    ISubscriptionRepository subscriptionRepository,
    IAdminPolicyRepository adminPolicyRepository,
    ISystemSettingsRepository systemSettingsRepository,
    ILoginAttemptRepository loginAttemptRepository) : ISystemAdministrationQueryService
{
    public Task<Branding?> GetBrandingAsync(CancellationToken cancellationToken = default) =>
        brandingRepository.GetSingletonAsync(cancellationToken);

    public Task<Subscription?> GetSubscriptionAsync(CancellationToken cancellationToken = default) =>
        subscriptionRepository.GetSingletonAsync(cancellationToken);

    public Task<AdminPolicy?> GetAdminPolicyAsync(CancellationToken cancellationToken = default) =>
        adminPolicyRepository.GetSingletonAsync(cancellationToken);

    public Task<SystemSettings?> GetSystemSettingsAsync(CancellationToken cancellationToken = default) =>
        systemSettingsRepository.GetSingletonAsync(cancellationToken);

    public Task<IReadOnlyList<LoginAttempt>> GetLoginAttemptsAsync(CancellationToken cancellationToken = default) =>
        loginAttemptRepository.ListOrderedAsync(cancellationToken);
}
