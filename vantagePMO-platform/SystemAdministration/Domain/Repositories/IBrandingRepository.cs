using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.SystemAdministration.Domain.Repositories;

public interface IBrandingRepository : IBaseRepository<Branding>
{
    Task<Branding?> GetSingletonAsync(CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
