using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;

namespace vantagePMO_platform.Dashboard.Domain.Repositories;

public interface IDepartmentRepository
{
    Task<IReadOnlyList<Department>> ListOrderedAsync(CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Department> departments, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
