using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using vantagePMO_platform.Iam.Application.Acl;
using vantagePMO_platform.Iam.Application.CommandServices;
using vantagePMO_platform.Iam.Application.Internal.CommandServices;
using vantagePMO_platform.Iam.Application.Internal.OutboundServices;
using vantagePMO_platform.Iam.Application.Internal.QueryServices;
using vantagePMO_platform.Iam.Application.QueryServices;
using vantagePMO_platform.Iam.Domain.Repositories;
using vantagePMO_platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using vantagePMO_platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using vantagePMO_platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using vantagePMO_platform.Iam.Infrastructure.Tokens.Jwt.Services;
using vantagePMO_platform.Iam.Interfaces.Acl;
using vantagePMO_platform.Profiles.Application.Acl;
using vantagePMO_platform.Profiles.Application.CommandServices;
using vantagePMO_platform.Profiles.Application.Internal.CommandServices;
using vantagePMO_platform.Profiles.Application.Internal.QueryServices;
using vantagePMO_platform.Profiles.Application.QueryServices;
using vantagePMO_platform.Profiles.Domain.Repositories;
using vantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Profiles.Interfaces.Acl;
using vantagePMO_platform.Projects.Application.CommandServices;
using vantagePMO_platform.Projects.Application.Internal.CommandServices;
using vantagePMO_platform.Projects.Application.Internal.QueryServices;
using vantagePMO_platform.Projects.Application.QueryServices;
using vantagePMO_platform.Projects.Domain.Repositories;
using vantagePMO_platform.Projects.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Dashboard.Application.Internal.CommandServices;
using vantagePMO_platform.Dashboard.Application.Internal.QueryServices;
using vantagePMO_platform.Dashboard.Domain.Repositories;
using vantagePMO_platform.Dashboard.Application.QueryServices;
using vantagePMO_platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.ChatHub.Application.CommandServices;
using vantagePMO_platform.ChatHub.Application.Internal.CommandServices;
using vantagePMO_platform.ChatHub.Application.Internal.QueryServices;
using vantagePMO_platform.ChatHub.Application.QueryServices;
using vantagePMO_platform.ChatHub.Domain.Repositories;
using vantagePMO_platform.ChatHub.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Reports.Application.Internal.CommandServices;
using vantagePMO_platform.Reports.Application.Internal.QueryServices;
using vantagePMO_platform.Reports.Application.QueryServices;
using vantagePMO_platform.Reports.Domain.Repositories;
using vantagePMO_platform.Reports.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Analytics.Application.Internal.CommandServices;
using vantagePMO_platform.Analytics.Application.Internal.QueryServices;
using vantagePMO_platform.Analytics.Application.QueryServices;
using vantagePMO_platform.Analytics.Domain.Repositories;
using vantagePMO_platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Routing: lowercase generated URLs.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Controllers with kebab-case route naming convention.
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Localization (resx live next to their classes, so ResourcesPath stays empty).
builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("es") };
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Database context (the AuditableEntityInterceptor is wired inside AppDbContext.OnConfiguring).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString);

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// Problem details for unhandled exceptions (RFC 7807).
builder.Services.AddProblemDetails();

// Swagger / OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Vantage PMO API",
        Version = "v1",
        Description = "Vantage PMO backend (DDD + CQRS, modular monolith)."
    });
    options.EnableAnnotations();
});

// Shared dependency injection.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<vantagePMO_platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory>();

// Profiles bounded context dependency injection.
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileStatsRepository, ProfileStatsRepository>();
builder.Services.AddScoped<IProfileSkillRepository, ProfileSkillRepository>();
builder.Services.AddScoped<IEndorsementRepository, EndorsementRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfileStatsQueryService, ProfileStatsQueryService>();
builder.Services.AddScoped<IProfileSkillQueryService, ProfileSkillQueryService>();
builder.Services.AddScoped<IEndorsementQueryService, EndorsementQueryService>();
builder.Services.AddScoped<ProfileRelatedDataSeeder>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

// Projects bounded context dependency injection.
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectCommandService, ProjectCommandService>();
builder.Services.AddScoped<IProjectQueryService, ProjectQueryService>();
builder.Services.AddScoped<ProjectsSampleDataSeeder>();

// Dashboard bounded context dependency injection.
builder.Services.AddScoped<IDashboardTaskRepository, DashboardTaskRepository>();
builder.Services.AddScoped<IScheduleItemRepository, ScheduleItemRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDashboardTaskQueryService, DashboardTaskQueryService>();
builder.Services.AddScoped<IScheduleItemQueryService, ScheduleItemQueryService>();
builder.Services.AddScoped<IDepartmentQueryService, DepartmentQueryService>();
builder.Services.AddScoped<DashboardSampleDataSeeder>();

// Chat Hub bounded context dependency injection.
builder.Services.AddScoped<IChatUserRepository, ChatUserRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IChatPinnedAssetRepository, ChatPinnedAssetRepository>();
builder.Services.AddScoped<IChatInsightRepository, ChatInsightRepository>();
builder.Services.AddScoped<IChatCommandService, ChatCommandService>();
builder.Services.AddScoped<IChatMessageCommandService, ChatMessageCommandService>();
builder.Services.AddScoped<IChatUserQueryService, ChatUserQueryService>();
builder.Services.AddScoped<IChatQueryService, ChatQueryService>();
builder.Services.AddScoped<IChatMessageQueryService, ChatMessageQueryService>();
builder.Services.AddScoped<IChatPinnedAssetQueryService, ChatPinnedAssetQueryService>();
builder.Services.AddScoped<IChatInsightQueryService, ChatInsightQueryService>();
builder.Services.AddScoped<ChatHubSampleDataSeeder>();

// Reports bounded context dependency injection.
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();
builder.Services.AddScoped<ReportsSampleDataSeeder>();

// Analytics bounded context dependency injection.
builder.Services.AddScoped<IAnalyticsDashboardRepository, AnalyticsDashboardRepository>();
builder.Services.AddScoped<IAnalyticsDashboardQueryService, AnalyticsDashboardQueryService>();
builder.Services.AddScoped<AnalyticsSampleDataSeeder>();

// IAM bounded context dependency injection.
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

var app = builder.Build();

// Apply pending migrations at startup.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    var projectsSampleDataSeeder = scope.ServiceProvider.GetRequiredService<ProjectsSampleDataSeeder>();
    await projectsSampleDataSeeder.SeedIfEmptyAsync();

    var dashboardSampleDataSeeder = scope.ServiceProvider.GetRequiredService<DashboardSampleDataSeeder>();
    await dashboardSampleDataSeeder.SeedIfEmptyAsync();

    var chatHubSampleDataSeeder = scope.ServiceProvider.GetRequiredService<ChatHubSampleDataSeeder>();
    await chatHubSampleDataSeeder.SeedIfEmptyAsync();

    var reportsSampleDataSeeder = scope.ServiceProvider.GetRequiredService<ReportsSampleDataSeeder>();
    await reportsSampleDataSeeder.SeedIfEmptyAsync();

    var analyticsSampleDataSeeder = scope.ServiceProvider.GetRequiredService<AnalyticsSampleDataSeeder>();
    await analyticsSampleDataSeeder.SeedIfEmptyAsync();
}

// Global exception handler must sit at the top of the pipeline.
app.UseGlobalExceptionHandler();

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseRequestAuthorization();
app.UseAuthorization();
app.MapControllers();

app.Run();
