using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.Commands;
using vantagePMO_platform.Projects.Domain.Model.Queries;

namespace vantagePMO_platform.Projects.Domain.Services;

public interface IProjectCommandService
{
    Task<Shared.Application.Model.Result<Project>> CreateProject(
        CreateProjectCommand command,
        CancellationToken cancellationToken = default);

    Task<Shared.Application.Model.Result<Project>> Handle(
        UpdateProjectCommand command,
        CancellationToken cancellationToken = default);
}
