using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.RiskCompliance.Domain.Model.Aggregates;
using vantagePMO_platform.RiskCompliance.Domain.Model.ValueObjects;

namespace vantagePMO_platform.RiskCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyRiskComplianceConfiguration(this ModelBuilder builder)
    {
        var heatmapConverter = CreateListConverter<HeatmapCell>();
        var heatmapComparer = CreateListComparer<HeatmapCell>();
        var alertsConverter = CreateListConverter<SystemIntegrityAlert>();
        var alertsComparer = CreateListComparer<SystemIntegrityAlert>();

        builder.Entity<RiskItem>(entity =>
        {
            entity.ToTable("risks");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Id).ValueGeneratedOnAdd();
            entity.Property(item => item.RiskId).HasMaxLength(40).IsRequired();
            entity.Property(item => item.Description).HasMaxLength(2000).IsRequired();
            entity.Property(item => item.Severity).HasMaxLength(40).IsRequired();
            entity.Property(item => item.Likelihood).HasMaxLength(40).IsRequired();
            entity.Property(item => item.Impact).HasMaxLength(40).IsRequired();
            entity.Property(item => item.Status).HasMaxLength(40).IsRequired();
            entity.Property(item => item.AuditTrail).HasMaxLength(200).IsRequired();
            entity.Property(item => item.AuditDate).HasMaxLength(40).IsRequired();
            entity.Property(item => item.FlaggedBy).HasMaxLength(120).IsRequired();
            entity.Property(item => item.Segment).HasMaxLength(120).IsRequired();
            entity.Property(item => item.ControlLog).HasMaxLength(200).IsRequired();
        });

        builder.Entity<RiskMatrix>(entity =>
        {
            entity.ToTable("risk_matrix");
            entity.HasKey(matrix => matrix.Id);
            entity.Property(matrix => matrix.Id).ValueGeneratedOnAdd();
            entity.Property(matrix => matrix.Segment).HasMaxLength(120).IsRequired();
            entity.Property(matrix => matrix.Environment).HasMaxLength(120).IsRequired();
            entity.Property(matrix => matrix.HeatmapCells)
                .HasConversion(heatmapConverter)
                .Metadata.SetValueComparer(heatmapComparer);
        });

        builder.Entity<ComplianceMetrics>(entity =>
        {
            entity.ToTable("compliance_metrics");
            entity.HasKey(metrics => metrics.Id);
            entity.Property(metrics => metrics.SystemIntegrityAlerts)
                .HasConversion(alertsConverter)
                .Metadata.SetValueComparer(alertsComparer);
        });
    }

    private static ValueConverter<List<T>, string> CreateListConverter<T>() =>
        new(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<T>>(value, (JsonSerializerOptions?)null) ?? new List<T>());

    private static ValueComparer<List<T>> CreateListComparer<T>() =>
        new(
            (left, right) => JsonSerializer.Serialize(left ?? new List<T>(), (JsonSerializerOptions?)null)
                == JsonSerializer.Serialize(right ?? new List<T>(), (JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<List<T>>(
                JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                (JsonSerializerOptions?)null) ?? new List<T>());
}
