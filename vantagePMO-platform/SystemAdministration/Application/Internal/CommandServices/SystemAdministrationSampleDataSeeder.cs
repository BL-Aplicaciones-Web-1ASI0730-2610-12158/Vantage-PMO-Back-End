using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;
using vantagePMO_platform.SystemAdministration.Domain.Repositories;

namespace vantagePMO_platform.SystemAdministration.Application.Internal.CommandServices;

/// <summary>
///     Seeds system administration data when tables are empty (matches front-end db.json).
/// </summary>
public class SystemAdministrationSampleDataSeeder(
    IBrandingRepository brandingRepository,
    ISubscriptionRepository subscriptionRepository,
    IAdminPolicyRepository adminPolicyRepository,
    ISystemSettingsRepository systemSettingsRepository,
    ILoginAttemptRepository loginAttemptRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (!await brandingRepository.AnyAsync(cancellationToken))
        {
            await brandingRepository.AddAsync(
                new Branding(
                    "Vantage PMO",
                    "Precision-driven project management for modern teams.",
                    "https://via.placeholder.com/200",
                    "#3b82f6",
                    "#10b981",
                    "Source Sans Pro",
                    "2026-01-15",
                    "2026-05-13"),
                cancellationToken);
        }

        if (!await subscriptionRepository.AnyAsync(cancellationToken))
        {
            await subscriptionRepository.AddAsync(
                new Subscription(
                    "professional",
                    45,
                    "monthly",
                    "2026-06-15",
                    "active",
                    "2026-01-15",
                    "2026-05-13"),
                cancellationToken);
        }

        if (!await adminPolicyRepository.AnyAsync(cancellationToken))
        {
            await adminPolicyRepository.AddAsync(
                new AdminPolicy(
                    "moderate",
                    true,
                    30,
                    1000,
                    true,
                    true,
                    true,
                    true,
                    "90 days",
                    true,
                    false,
                    12,
                    true,
                    true,
                    "2026-01-15",
                    "2026-05-13"),
                cancellationToken);
        }

        if (!await systemSettingsRepository.AnyAsync(cancellationToken))
        {
            await systemSettingsRepository.AddAsync(
                new SystemSettings(
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    true,
                    false,
                    true,
                    false,
                    true,
                    true,
                    true,
                    true,
                    "2026-01-15",
                    "2026-05-13"),
                cancellationToken);
        }

        if (!await loginAttemptRepository.AnyAsync(cancellationToken))
        {
            await loginAttemptRepository.AddRangeAsync(
            [
                new LoginAttempt("alex.sterling@vantagepmo.io", "2026-05-13T14:30:00Z", "success"),
                new LoginAttempt("sarah.johnson@vantagepmo.io", "2026-05-13T14:25:00Z", "success"),
                new LoginAttempt("marcus.lee@vantagepmo.io", "2026-05-13T14:15:00Z", "failed"),
                new LoginAttempt("emily.watson@vantagepmo.io", "2026-05-13T14:10:00Z", "success"),
                new LoginAttempt("daniel.brooks@vantagepmo.io", "2026-05-13T14:05:00Z", "success")
            ],
                cancellationToken);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
