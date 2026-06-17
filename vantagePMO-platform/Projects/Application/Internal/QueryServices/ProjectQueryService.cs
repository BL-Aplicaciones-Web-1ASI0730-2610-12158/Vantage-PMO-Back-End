using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.Queries;
using vantagePMO_platform.Projects.Domain.Repositories;
using vantagePMO_platform.Projects.Domain.Services;

namespace vantagePMO_platform.Projects.Application.Internal.QueryServices;

public class ProjectQueryService(IProjectRepository projectRepository) : IProjectQueryService
{
    public async Task<IReadOnlyList<Project>> Handle(
        GetAllProjectsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await projectRepository.ListOrderedAsync(cancellationToken);
    }

    public async Task<Project?> Handle(
        GetProjectByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await projectRepository.FindByIdAsync(query.ProjectId, cancellationToken);
    }
}
