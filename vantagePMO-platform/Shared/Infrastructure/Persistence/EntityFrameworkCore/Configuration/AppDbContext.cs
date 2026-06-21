using VantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.ChatHub.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using vantagePMO_platform.Reports.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

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

        // Chat Hub Context
        builder.ApplyChatHubConfiguration();

        // Reports Context
        builder.ApplyReportsConfiguration();

        // Analytics Context
        builder.ApplyAnalyticsConfiguration();

        // IAM Context
        // builder.ApplyIamConfiguration(); // TODO: enable when the IAM bounded context is implemented.

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
