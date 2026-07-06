using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.Dashboard.Domain.Model.Aggregates;

namespace vantagePMO_platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDashboardConfiguration(this ModelBuilder builder)
    {
        var avatarSeedsConverter = new ValueConverter<List<string>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<string>>(value, (JsonSerializerOptions?)null) ?? new List<string>());

        var avatarSeedsComparer = new ValueComparer<List<string>>(
            (left, right) => (left ?? new List<string>()).SequenceEqual(right ?? new List<string>()),
            value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
            value => value.ToList());

        builder.Entity<DashboardTask>(task =>
        {
            task.ToTable("dashboard_tasks");
            task.HasKey(entity => entity.Id);
            task.Property(entity => entity.Id).ValueGeneratedOnAdd();
            task.Property(entity => entity.Title).HasMaxLength(300).IsRequired();
            task.Property(entity => entity.Assignee).HasMaxLength(120);
            task.Property(entity => entity.Department).HasMaxLength(120);
            task.Property(entity => entity.Priority).HasMaxLength(60);
            task.Property(entity => entity.Icon).HasMaxLength(80);
            task.Property(entity => entity.IconBg).HasMaxLength(40);
            task.Property(entity => entity.AvatarSeeds)
                .HasConversion(avatarSeedsConverter)
                .Metadata.SetValueComparer(avatarSeedsComparer);
        });

        builder.Entity<ScheduleItem>(item =>
        {
            item.ToTable("schedules");
            item.HasKey(entity => entity.Id);
            item.Property(entity => entity.Id).ValueGeneratedOnAdd();
            item.Property(entity => entity.Date).HasMaxLength(20);
            item.Property(entity => entity.Time).HasMaxLength(20);
            item.Property(entity => entity.Title).HasMaxLength(200).IsRequired();
            item.Property(entity => entity.Detail).HasMaxLength(200);
            item.Property(entity => entity.Type).HasMaxLength(40);
        });

        builder.Entity<Department>(department =>
        {
            department.ToTable("departments");
            department.HasKey(entity => entity.Id);
            department.Property(entity => entity.Id).ValueGeneratedOnAdd();
            department.Property(entity => entity.Name).HasMaxLength(120).IsRequired();
            department.Property(entity => entity.Percent);
        });
    }
}
