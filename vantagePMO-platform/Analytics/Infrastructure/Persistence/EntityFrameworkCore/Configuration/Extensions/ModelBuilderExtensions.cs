using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.Analytics.Domain.Model.Aggregates;
using vantagePMO_platform.Analytics.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAnalyticsConfiguration(this ModelBuilder builder)
    {
        var monthlyExpendituresConverter = CreateListConverter<MonthlyExpenditure>();
        var resourceSaturationConverter = CreateListConverter<DepartmentCapacity>();
        var topMoversConverter = CreateListConverter<TopMover>();
        var portfolioRoiConverter = CreateObjectConverter<PortfolioRoi>();
        var summaryKpisConverter = CreateObjectConverter<SummaryKpis>();

        var monthlyExpendituresComparer = CreateListComparer<MonthlyExpenditure>();
        var resourceSaturationComparer = CreateListComparer<DepartmentCapacity>();
        var topMoversComparer = CreateListComparer<TopMover>();
        var portfolioRoiComparer = CreateObjectComparer<PortfolioRoi>();
        var summaryKpisComparer = CreateObjectComparer<SummaryKpis>();

        builder.Entity<AnalyticsDashboard>(dashboard =>
        {
            dashboard.ToTable("analytics");
            dashboard.HasKey(entity => entity.Id);
            dashboard.Property(entity => entity.Id).ValueGeneratedOnAdd();

            dashboard.Property(entity => entity.MonthlyExpenditures)
                .HasConversion(monthlyExpendituresConverter)
                .Metadata.SetValueComparer(monthlyExpendituresComparer);

            dashboard.Property(entity => entity.PortfolioRoi)
                .HasConversion(portfolioRoiConverter)
                .Metadata.SetValueComparer(portfolioRoiComparer);

            dashboard.Property(entity => entity.ResourceSaturation)
                .HasConversion(resourceSaturationConverter)
                .Metadata.SetValueComparer(resourceSaturationComparer);

            dashboard.Property(entity => entity.TopMovers)
                .HasConversion(topMoversConverter)
                .Metadata.SetValueComparer(topMoversComparer);

            dashboard.Property(entity => entity.SummaryKpis)
                .HasConversion(summaryKpisConverter)
                .Metadata.SetValueComparer(summaryKpisComparer);
        });
    }

    private static ValueConverter<List<T>, string> CreateListConverter<T>() =>
        new(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<T>>(value, (JsonSerializerOptions?)null) ?? new List<T>());

    private static ValueConverter<T, string> CreateObjectConverter<T>() =>
        new(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<T>(value, (JsonSerializerOptions?)null)!);

    private static ValueComparer<List<T>> CreateListComparer<T>() =>
        new(
            (left, right) => JsonSerializer.Serialize(left ?? new List<T>(), (JsonSerializerOptions?)null)
                == JsonSerializer.Serialize(right ?? new List<T>(), (JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<List<T>>(
                JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                (JsonSerializerOptions?)null) ?? new List<T>());

    private static ValueComparer<T> CreateObjectComparer<T>() =>
        new(
            (left, right) => JsonSerializer.Serialize(left, (JsonSerializerOptions?)null)
                == JsonSerializer.Serialize(right, (JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<T>(
                JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                (JsonSerializerOptions?)null)!);
}
