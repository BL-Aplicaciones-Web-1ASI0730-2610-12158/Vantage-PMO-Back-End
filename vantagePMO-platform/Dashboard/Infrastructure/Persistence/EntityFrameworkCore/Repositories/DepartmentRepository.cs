using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DepartmentRepository(AppDbContext context) : IDepartmentRepository
{
    public async Task<IReadOnlyList<Department>> ListOrderedAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Department>()
            .OrderBy(department => department.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Department> departments, CancellationToken cancellationToken = default)
    {
        await context.Set<Department>().AddRangeAsync(departments, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Department>().AnyAsync(cancellationToken);
    }
}
