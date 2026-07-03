using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Settings.Domain.Model.Aggregates;

namespace vantagePMO_platform.Settings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySettingsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<UserSettings>(settings =>
        {
            settings.ToTable("user_settings");
            settings.HasKey(entity => entity.Id);
            settings.Property(entity => entity.Id).ValueGeneratedNever();
            settings.Property(entity => entity.Theme).HasMaxLength(20).IsRequired();
            settings.Property(entity => entity.Language).HasMaxLength(10).IsRequired();
            settings.Property(entity => entity.Timezone).HasMaxLength(60).IsRequired();
            settings.Property(entity => entity.DateFormat).HasMaxLength(20).IsRequired();
            settings.Property(entity => entity.TimeFormat).HasMaxLength(10).IsRequired();
            settings.Property(entity => entity.FirstDayOfWeek).HasMaxLength(20).IsRequired();
            settings.Property(entity => entity.Currency).HasMaxLength(10).IsRequired();
            settings.Property(entity => entity.AccentColor).HasMaxLength(20).IsRequired();
            settings.Property(entity => entity.Density).HasMaxLength(20).IsRequired();
            settings.Property(entity => entity.ProfileVisibility).HasMaxLength(20).IsRequired();
            settings.Property(entity => entity.DisplayName).HasMaxLength(120).IsRequired();
            settings.Property(entity => entity.JobTitle).HasMaxLength(120).IsRequired();
            settings.Property(entity => entity.Bio).HasMaxLength(2000);
            settings.Property(entity => entity.Phone).HasMaxLength(40);
            settings.Property(entity => entity.Department).HasMaxLength(80).IsRequired();
            settings.Property(entity => entity.UpdatedAt).HasMaxLength(20).IsRequired();
        });
    }
}
