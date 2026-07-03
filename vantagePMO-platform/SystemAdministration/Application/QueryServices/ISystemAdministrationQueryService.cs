using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

namespace vantagePMO_platform.SystemAdministration.Application.QueryServices;

public interface ISystemAdministrationQueryService
{
    Task<Branding?> GetBrandingAsync(CancellationToken cancellationToken = default);
    Task<Subscription?> GetSubscriptionAsync(CancellationToken cancellationToken = default);
    Task<AdminPolicy?> GetAdminPolicyAsync(CancellationToken cancellationToken = default);
    Task<SystemSettings?> GetSystemSettingsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LoginAttempt>> GetLoginAttemptsAsync(CancellationToken cancellationToken = default);
}
