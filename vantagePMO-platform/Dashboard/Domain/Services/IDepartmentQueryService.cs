using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;
using vantagePMO_platform.Dashboard.Domain.Model.Queries;

namespace vantagePMO_platform.Dashboard.Domain.Services;

public interface IDepartmentQueryService
{
    Task<IReadOnlyList<Department>> Handle(
        GetAllDepartmentsQuery query,
        CancellationToken cancellationToken = default);
}
