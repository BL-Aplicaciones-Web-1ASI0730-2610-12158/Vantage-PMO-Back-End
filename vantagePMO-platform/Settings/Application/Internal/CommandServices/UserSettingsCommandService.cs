using vantagePMO_platform.Settings.Application.CommandServices;
using vantagePMO_platform.Settings.Domain.Model.Aggregates;
using vantagePMO_platform.Settings.Domain.Model.Commands;
using vantagePMO_platform.Settings.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Settings.Application.Internal.CommandServices;

public class UserSettingsCommandService(
    IUserSettingsRepository userSettingsRepository,
    IUnitOfWork unitOfWork) : IUserSettingsCommandService
{
    public async Task<UserSettings?> UpdateAsync(
        UpdateUserSettingsCommand command,
        CancellationToken cancellationToken = default)
    {
        var settings = await userSettingsRepository.GetSingletonAsync(cancellationToken);
        if (settings is null)
            return null;

        settings.Update(command);
        userSettingsRepository.Update(settings);
        await unitOfWork.CompleteAsync(cancellationToken);

        return settings;
    }
}
