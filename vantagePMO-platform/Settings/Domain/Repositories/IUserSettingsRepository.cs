using vantagePMO_platform.Settings.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Settings.Domain.Repositories;

public interface IUserSettingsRepository : IBaseRepository<UserSettings>
{
    Task<UserSettings?> GetSingletonAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
