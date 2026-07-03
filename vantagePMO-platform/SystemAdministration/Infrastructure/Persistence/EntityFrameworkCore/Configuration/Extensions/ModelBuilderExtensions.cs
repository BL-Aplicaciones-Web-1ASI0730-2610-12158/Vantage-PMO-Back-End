using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

namespace vantagePMO_platform.SystemAdministration.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySystemAdministrationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Branding>(entity =>
        {
            entity.ToTable("branding");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Id).ValueGeneratedNever();
            entity.Property(item => item.CompanyName).HasMaxLength(200).IsRequired();
            entity.Property(item => item.CompanyDescription).HasMaxLength(2000);
            entity.Property(item => item.LogoUrl).HasMaxLength(500);
            entity.Property(item => item.PrimaryColor).HasMaxLength(20).IsRequired();
            entity.Property(item => item.SecondaryColor).HasMaxLength(20).IsRequired();
            entity.Property(item => item.TypographyStyle).HasMaxLength(80);
            entity.Property(item => item.CreatedAt).HasMaxLength(20).IsRequired();
            entity.Property(item => item.UpdatedAt).HasMaxLength(20).IsRequired();
        });

        builder.Entity<Subscription>(entity =>
        {
            entity.ToTable("subscriptions");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Id).ValueGeneratedNever();
            entity.Property(item => item.Plan).HasMaxLength(40).IsRequired();
            entity.Property(item => item.BillingCycle).HasMaxLength(20).IsRequired();
            entity.Property(item => item.ExpirationDate).HasMaxLength(20).IsRequired();
            entity.Property(item => item.Status).HasMaxLength(20).IsRequired();
            entity.Property(item => item.CreatedAt).HasMaxLength(20).IsRequired();
            entity.Property(item => item.UpdatedAt).HasMaxLength(20).IsRequired();
        });

        builder.Entity<AdminPolicy>(entity =>
        {
            entity.ToTable("admin_policies");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Id).ValueGeneratedNever();
            entity.Property(item => item.PasswordPolicy).HasMaxLength(40).IsRequired();
            entity.Property(item => item.PasswordExpiration).HasMaxLength(40);
            entity.Property(item => item.CreatedAt).HasMaxLength(20).IsRequired();
            entity.Property(item => item.UpdatedAt).HasMaxLength(20).IsRequired();
        });

        builder.Entity<SystemSettings>(entity =>
        {
            entity.ToTable("system_settings");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Id).ValueGeneratedNever();
            entity.Property(item => item.CreatedAt).HasMaxLength(20).IsRequired();
            entity.Property(item => item.UpdatedAt).HasMaxLength(20).IsRequired();
        });

        builder.Entity<LoginAttempt>(entity =>
        {
            entity.ToTable("login_attempts");
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Id).ValueGeneratedOnAdd();
            entity.Property(item => item.User).HasMaxLength(200).IsRequired();
            entity.Property(item => item.Timestamp).HasMaxLength(40).IsRequired();
            entity.Property(item => item.Status).HasMaxLength(20).IsRequired();
        });
    }
}
