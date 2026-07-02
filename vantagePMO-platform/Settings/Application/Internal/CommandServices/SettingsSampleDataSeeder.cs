using vantagePMO_platform.Settings.Domain.Model.Aggregates;
using vantagePMO_platform.Settings.Domain.Model.Commands;
using vantagePMO_platform.Settings.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Settings.Application.Internal.CommandServices;

/// <summary>
///     Seeds default user settings when the table is empty (matches front-end db.json).
/// </summary>
public class SettingsSampleDataSeeder(
    IUserSettingsRepository userSettingsRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (await userSettingsRepository.AnyAsync(cancellationToken))
            return;

        var settings = new UserSettings(new CreateUserSettingsCommand(
            "light",
            "en",
            "America/New_York",
            "MM/DD/YYYY",
            "12h",
            "Sunday",
            "USD",
            "#3b82f6",
            "comfortable",
            EmailNotifications: true,
            PushNotifications: true,
            WeeklyDigest: false,
            MentionAlerts: true,
            TwoFactorEnabled: false,
            "team",
            "Alex Sterling",
            "Lead Architect",
            "Precision-driven PMO leader with a focus on strategic execution and team performance.",
            "+1 (555) 234-5678",
            "Executive",
            "2026-05-14"));

        await userSettingsRepository.AddAsync(settings, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
