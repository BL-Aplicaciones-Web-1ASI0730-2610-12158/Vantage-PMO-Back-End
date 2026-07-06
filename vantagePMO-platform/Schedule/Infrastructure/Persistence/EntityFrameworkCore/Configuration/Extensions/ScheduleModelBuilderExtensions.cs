using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.Schedule.Domain.Model.Aggregates;

namespace vantagePMO_platform.Schedule.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ScheduleModelBuilderExtensions
{
    public static void ConfigureScheduleAggregates(this ModelBuilder builder)
    {
        var timeConverter = new ValueConverter<TimeOnly, TimeSpan>(
            time => time.ToTimeSpan(),
            value => TimeOnly.FromTimeSpan(value));

        builder.Entity<ScheduleEntry>(entity =>
        {
            entity.ToTable("user_schedule_entries");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId)
                .IsRequired();

            entity.Property(e => e.Date)
                .IsRequired();

            entity.Property(e => e.Time)
                .HasConversion(timeConverter)
                .IsRequired();

            entity.Property(e => e.Duration)
                .IsRequired()
                .HasDefaultValue(60);

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Detail)
                .HasMaxLength(1000);

            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValue(true);

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => new { e.UserId, e.Date });
            entity.HasIndex(e => e.Active);
        });
    }
}
