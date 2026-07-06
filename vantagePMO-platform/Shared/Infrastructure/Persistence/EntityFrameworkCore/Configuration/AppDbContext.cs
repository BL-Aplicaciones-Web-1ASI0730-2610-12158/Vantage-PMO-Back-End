using vantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Projects.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.ChatHub.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Reports.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Meetings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Support.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Settings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.SystemAdministration.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.TaskCollaboration.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Workspace.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Schedule.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Schedule.Domain.Model.Aggregates;

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    ///     DbSet for Schedule aggregates.
    /// </summary>
    public DbSet<ScheduleEntry> Schedules { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Profiles Context
        builder.ApplyProfilesConfiguration();

        // Projects Context
        builder.ApplyProjectsConfiguration();

        // Dashboard Context
        builder.ApplyDashboardConfiguration();

        // Chat Hub Context
        builder.ApplyChatHubConfiguration();

        // Reports Context
        builder.ApplyReportsConfiguration();

        // Analytics Context
        builder.ApplyAnalyticsConfiguration();

        // IAM Context
        builder.ApplyIamConfiguration();
        
        // Meetings Context
        builder.ApplyMeetingsConfiguration();

        // Support Context
        builder.ApplySupportConfiguration();

        // Settings Context
        builder.ApplySettingsConfiguration();

        // System Administration Context
        builder.ApplySystemAdministrationConfiguration();

        // Task Collaboration Context
        builder.ApplyTaskCollaborationConfiguration();

        // Workspace Context
        builder.ApplyWorkspaceConfiguration();

        // Schedule Context
        builder.ConfigureScheduleAggregates();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
