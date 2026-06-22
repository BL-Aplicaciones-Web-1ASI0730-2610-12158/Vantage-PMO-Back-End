using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.Reports.Domain.Model.Aggregates;

namespace vantagePMO_platform.Reports.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyReportsConfiguration(this ModelBuilder builder)
    {
        var velocityTrendConverter = new ValueConverter<List<int>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<int>>(value, (JsonSerializerOptions?)null) ?? new List<int>());

        var velocityTrendComparer = new ValueComparer<List<int>>(
            (left, right) => (left ?? new List<int>()).SequenceEqual(right ?? new List<int>()),
            value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
            value => value.ToList());

        builder.Entity<Report>(report =>
        {
            report.ToTable("reports");
            report.HasKey(entity => entity.Id);
            report.Property(entity => entity.Id).ValueGeneratedOnAdd();
            report.Property(entity => entity.Project).HasMaxLength(200).IsRequired();
            report.Property(entity => entity.Label).HasMaxLength(20);
            report.Property(entity => entity.Manager).HasMaxLength(120);
            report.Property(entity => entity.Status).HasMaxLength(40);
            report.Property(entity => entity.Period).HasMaxLength(40);
            report.Property(entity => entity.GeneratedAt).HasMaxLength(20);
            report.Property(entity => entity.Attachment).HasMaxLength(120);
            report.Property(entity => entity.Type).HasMaxLength(80);
            report.Property(entity => entity.AiInsight).HasMaxLength(2000);
            report.Property(entity => entity.VelocityTrend)
                .HasConversion(velocityTrendConverter)
                .Metadata.SetValueComparer(velocityTrendComparer);
        });
    }
}
