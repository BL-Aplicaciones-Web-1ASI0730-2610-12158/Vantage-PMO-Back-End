using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using vantagePMO_platform.Projects.Application.Errors;
using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.Commands;
using vantagePMO_platform.Projects.Domain.Repositories;
using vantagePMO_platform.Projects.Domain.Services;
using vantagePMO_platform.Shared.Application.Model;
using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.Shared.Resources.Errors;

namespace vantagePMO_platform.Projects.Application.Internal.CommandServices;

public class ProjectCommandService(
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer,
    ILogger<ProjectCommandService> logger) : IProjectCommandService
{
    public async Task<Result<Project>> CreateProject(
        CreateProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result<Project>.Failure(
                ProjectsError.InvalidProjectData,
                localizer["ProjectsError.InvalidProjectData"]);
        }

        try
        {
            var project = new Project(command);
            await projectRepository.AddAsync(project, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            logger.LogInformation("Project {ProjectId} created.", project.Id);
            return Result<Project>.Success(project);
        }
        catch (OperationCanceledException)
        {
            return Result<Project>.Failure(
                ProjectsError.OperationCancelled,
                localizer["ProjectsError.OperationCancelled"]);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Database error while creating project.");
            return Result<Project>.Failure(
                ProjectsError.DatabaseError,
                localizer["ProjectsError.DatabaseError"]);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error while creating project.");
            return Result<Project>.Failure(
                ProjectsError.InternalServerError,
                localizer["ProjectsError.InternalServerError"]);
        }
    }

    public async Task<Result<Project>> Handle(
        UpdateProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result<Project>.Failure(
                ProjectsError.InvalidProjectData,
                localizer["ProjectsError.InvalidProjectData"]);
        }

        try
        {
            var project = await projectRepository.FindByIdAsync(command.ProjectId, cancellationToken);
            if (project is null)
            {
                return Result<Project>.Failure(
                    ProjectsError.ProjectNotFound,
                    localizer["ProjectsError.ProjectNotFound"]);
            }

            project.Update(command);
            projectRepository.Update(project);
            await unitOfWork.CompleteAsync(cancellationToken);

            logger.LogInformation("Project {ProjectId} updated.", project.Id);
            return Result<Project>.Success(project);
        }
        catch (OperationCanceledException)
        {
            return Result<Project>.Failure(
                ProjectsError.OperationCancelled,
                localizer["ProjectsError.OperationCancelled"]);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Database error while updating project {ProjectId}.", command.ProjectId);
            return Result<Project>.Failure(
                ProjectsError.DatabaseError,
                localizer["ProjectsError.DatabaseError"]);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error while updating project {ProjectId}.", command.ProjectId);
            return Result<Project>.Failure(
                ProjectsError.InternalServerError,
                localizer["ProjectsError.InternalServerError"]);
        }
    }
}
