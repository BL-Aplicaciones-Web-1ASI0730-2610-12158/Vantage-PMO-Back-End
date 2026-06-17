using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.Commands;
using vantagePMO_platform.Projects.Domain.Model.ValueObjects;
using vantagePMO_platform.Projects.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Projects.Application.Internal.CommandServices;

/// <summary>
///     Seeds sample projects when the database has none (development convenience).
/// </summary>
public class ProjectsSampleDataSeeder(
    AppDbContext context,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (await context.Set<Project>().AnyAsync(cancellationToken))
            return;

        var samples = new[]
        {
            new Project(new CreateProjectCommand(
                "Aura Mixed-Use",
                "Commercial Real Estate",
                "Mixed-use development with retail, residential, and office components.",
                64,
                "At Risk",
                "2025-06-01",
                null,
                "Delayed: Supply",
                "Julian Thorne",
                1,
                new[]
                {
                    new TeamMember { Id = 1, Name = "Alexander", Avatar = "A" },
                    new TeamMember { Id = 2, Name = "Benjamin", Avatar = "B" }
                },
                new[]
                {
                    new Milestone { Id = 1, Name = "Final Audit Submission", Date = "Oct 14", Type = "warning" },
                    new Milestone { Id = 2, Name = "Staging Deployment", Date = "Oct 28", Type = "pending" }
                })),
            new Project(new CreateProjectCommand(
                "Nexus Data Hub",
                "Infrastructure Modernization",
                "Centralized data infrastructure hub for enterprise analytics.",
                82,
                "Healthy",
                "2025-03-01",
                null,
                "Due Oct 12",
                "Julian Thorne",
                1,
                new[]
                {
                    new TeamMember { Id = 1, Name = "Diana", Avatar = "D" },
                    new TeamMember { Id = 2, Name = "Elena", Avatar = "E" }
                },
                new[]
                {
                    new Milestone { Id = 1, Name = "Interior Fit-out Done", Date = "Today", Type = "success" },
                    new Milestone { Id = 2, Name = "Grand Opening", Date = "Nov 05", Type = "pending" }
                }))
        };

        foreach (var project in samples)
            await projectRepository.AddAsync(project, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
