using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Projects.Domain.Repositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<IReadOnlyList<Project>> ListOrderedAsync(CancellationToken cancellationToken = default);
}
