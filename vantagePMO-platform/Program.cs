using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using VantagePMO_platform.Profiles.Application.Internal.CommandServices;
using VantagePMO_platform.Profiles.Application.Internal.QueryServices;
using VantagePMO_platform.Profiles.Domain.Repositories;
using VantagePMO_platform.Profiles.Domain.Services;
using VantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Analytics.Application.Internal.CommandServices;
using vantagePMO_platform.Analytics.Application.Internal.QueryServices;
using vantagePMO_platform.Analytics.Application.QueryServices;
using vantagePMO_platform.Analytics.Domain.Repositories;
using vantagePMO_platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
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
using vantagePMO_platform.Meetings.Application.CommandServices;
using vantagePMO_platform.Meetings.Application.Internal.CommandServices;
using vantagePMO_platform.Meetings.Application.Internal.QueryServices;
using vantagePMO_platform.Meetings.Application.QueryServices;
using vantagePMO_platform.Meetings.Domain.Repositories;
using vantagePMO_platform.Meetings.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using VantagePMO_platform.Shared.Domain.Repositories;
using VantagePMO_platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using VantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using VantagePMO_platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Routing: lowercase generated URLs.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Controllers with kebab-case route naming convention.
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
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
builder.Services.AddScoped<VantagePMO_platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory>();

// Profiles bounded context dependency injection.
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

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

// Meetings bounded context dependency injection.
builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<IMeetingCommandService, MeetingCommandService>();
builder.Services.AddScoped<IMeetingQueryService, MeetingQueryService>();

var app = builder.Build();

// Apply pending migrations at startup.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

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
app.UseAuthorization();
app.MapControllers();

app.Run();
