using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.Queries;

namespace vantagePMO_platform.Projects.Domain.Services;

public interface IProjectQueryService
{
    Task<IReadOnlyList<Project>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken = default);

    Task<Project?> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken = default);
}
