using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

namespace vantagePMO_platform.SystemAdministration.Application.CommandServices;

public interface ISystemAdministrationCommandService
{
    Task<Branding?> UpdateBrandingAsync(UpdateBrandingCommand command, CancellationToken cancellationToken = default);
    Task<Subscription?> UpdateSubscriptionAsync(UpdateSubscriptionCommand command, CancellationToken cancellationToken = default);
    Task<Subscription?> RenewSubscriptionAsync(int subscriptionId, CancellationToken cancellationToken = default);
    Task<AdminPolicy?> UpdateAdminPolicyAsync(UpdateAdminPolicyCommand command, CancellationToken cancellationToken = default);
    Task<SystemSettings?> UpdateSystemSettingsAsync(UpdateSystemSettingsCommand command, CancellationToken cancellationToken = default);
}
