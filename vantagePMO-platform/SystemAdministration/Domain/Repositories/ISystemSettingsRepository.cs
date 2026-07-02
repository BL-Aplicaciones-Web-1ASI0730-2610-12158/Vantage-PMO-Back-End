using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.SystemAdministration.Domain.Repositories;

public interface ISystemSettingsRepository : IBaseRepository<SystemSettings>
{
    Task<SystemSettings?> GetSingletonAsync(CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
