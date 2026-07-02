using vantagePMO_platform.Settings.Domain.Model.Aggregates;
using vantagePMO_platform.Settings.Domain.Model.Commands;

namespace vantagePMO_platform.Settings.Application.CommandServices;

public interface IUserSettingsCommandService
{
    Task<UserSettings?> UpdateAsync(UpdateUserSettingsCommand command, CancellationToken cancellationToken = default);
}
