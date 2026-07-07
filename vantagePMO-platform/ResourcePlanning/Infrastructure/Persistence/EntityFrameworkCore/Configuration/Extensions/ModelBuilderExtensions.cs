using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.ResourcePlanning.Domain.Model.Aggregates;
using vantagePMO_platform.ResourcePlanning.Domain.Model.ValueObjects;

namespace vantagePMO_platform.ResourcePlanning.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyResourcePlanningConfiguration(this ModelBuilder builder)
    {
        var departmentCapacityConverter = CreateListConverter<DepartmentUtilization>();
        var allocationsConverter = CreateListConverter<ResourceAllocation>();
        var capacityGapsConverter = CreateListConverter<CapacityGap>();
        var summaryKpisConverter = CreateObjectConverter<PlanningSummary>();

        var departmentCapacityComparer = CreateListComparer<DepartmentUtilization>();
        var allocationsComparer = CreateListComparer<ResourceAllocation>();
        var capacityGapsComparer = CreateListComparer<CapacityGap>();
        var summaryKpisComparer = CreateObjectComparer<PlanningSummary>();

        builder.Entity<ResourcePlanningDashboard>(dashboard =>
        {
            dashboard.ToTable("resource_plannings");
            dashboard.HasKey(entity => entity.Id);
            dashboard.Property(entity => entity.Id).ValueGeneratedOnAdd();
            dashboard.Property(entity => entity.Period).HasMaxLength(32);

            dashboard.Property(entity => entity.SummaryKpis)
                .HasConversion(summaryKpisConverter)
                .Metadata.SetValueComparer(summaryKpisComparer);

            dashboard.Property(entity => entity.DepartmentCapacity)
                .HasConversion(departmentCapacityConverter)
                .Metadata.SetValueComparer(departmentCapacityComparer);

            dashboard.Property(entity => entity.Allocations)
                .HasConversion(allocationsConverter)
                .Metadata.SetValueComparer(allocationsComparer);

            dashboard.Property(entity => entity.CapacityGaps)
                .HasConversion(capacityGapsConverter)
                .Metadata.SetValueComparer(capacityGapsComparer);
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
