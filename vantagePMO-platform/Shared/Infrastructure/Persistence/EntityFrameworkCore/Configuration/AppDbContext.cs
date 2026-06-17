using vantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Projects.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
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

        // IAM Context
        builder.ApplyIamConfiguration();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
