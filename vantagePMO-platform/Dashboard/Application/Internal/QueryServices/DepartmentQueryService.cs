using vantagePMO_platform.Dashboard.Domain.Model.Queries;
using vantagePMO_platform.Dashboard.Domain.Repositories;
using vantagePMO_platform.Dashboard.Application.QueryServices;

namespace vantagePMO_platform.Dashboard.Application.Internal.QueryServices;

public class DepartmentQueryService(IDepartmentRepository departmentRepository) : IDepartmentQueryService
{
    public async Task<IReadOnlyList<Domain.Model.Aggregates.Department>> Handle(
        GetAllDepartmentsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await departmentRepository.ListOrderedAsync(cancellationToken);
    }
}
