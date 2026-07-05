using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.Profiles.Domain.Model.Aggregates;
using vantagePMO_platform.Profiles.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions that configure the Profiles bounded context schema.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the EF Core configuration for the Profiles bounded context.
    /// </summary>
    /// <param name="builder">The model builder for the shared database context.</param>
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        var stringListConverter = new ValueConverter<List<string>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<string>>(value, (JsonSerializerOptions?)null) ?? new List<string>());

        var stringListComparer = new ValueComparer<List<string>>(
            (left, right) => (left ?? new List<string>()).SequenceEqual(right ?? new List<string>()),
            value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
            value => value.ToList());

        builder.Entity<Profile>(profile =>
        {
            profile.HasKey(p => p.Id);
            profile.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            profile.Property(p => p.Name)
                .HasConversion(name => name.FullName, value => new PersonName(value))
                .HasMaxLength(PersonName.MaxLength)
                .IsRequired();

            profile.Property(p => p.Email)
                .HasConversion(email => email.Value, value => new EmailAddress(value))
                .HasMaxLength(EmailAddress.MaxLength)
                .IsRequired();

            profile.HasIndex(p => p.Email).IsUnique();

            profile.Property(p => p.UserId).IsRequired();
            profile.HasIndex(p => p.UserId).IsUnique();

            profile.Property(p => p.Role).HasMaxLength(120);
            profile.Property(p => p.DateOfBirth)
                .HasConversion(
                    date => date.HasValue ? date.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                    value => value.HasValue ? DateOnly.FromDateTime(value.Value) : null);
            profile.Property(p => p.Department).HasMaxLength(120);
            profile.Property(p => p.Joined).HasMaxLength(60);
            profile.Property(p => p.AvatarSeed).HasMaxLength(120);
            profile.Property(p => p.Availability).HasMaxLength(120);
            profile.Property(p => p.Experience).HasMaxLength(60);
            profile.Property(p => p.DeliveryRate).HasMaxLength(60);
            profile.Property(p => p.ActiveBudget).HasMaxLength(60);
            profile.Property(p => p.SkillsDescription).HasMaxLength(1000);
            profile.Property(p => p.Location).HasMaxLength(160);
            profile.Property(p => p.YearsActive).HasMaxLength(120);
            profile.Property(p => p.AvailabilityLabel).HasMaxLength(120);

            profile.Property(p => p.Bio)
                .HasConversion(stringListConverter)
                .Metadata.SetValueComparer(stringListComparer);

            profile.Property(p => p.Certifications)
                .HasConversion(stringListConverter)
                .Metadata.SetValueComparer(stringListComparer);
        });

        builder.Entity<ProfileStats>(stats =>
        {
            stats.HasKey(entity => entity.Id);
            stats.Property(entity => entity.Id).ValueGeneratedOnAdd();
            stats.Property(entity => entity.UserId).IsRequired();
            stats.HasIndex(entity => entity.UserId).IsUnique();
            stats.Property(entity => entity.PortfolioHealth).HasMaxLength(60);
        });

        builder.Entity<ProfileSkill>(skill =>
        {
            skill.HasKey(entity => entity.Id);
            skill.Property(entity => entity.Id).ValueGeneratedOnAdd();
            skill.Property(entity => entity.UserId).IsRequired();
            skill.Property(entity => entity.Name).HasMaxLength(120).IsRequired();
            skill.Property(entity => entity.Percentage).IsRequired();
            skill.HasIndex(entity => entity.UserId);
        });

        builder.Entity<Endorsement>(endorsement =>
        {
            endorsement.HasKey(entity => entity.Id);
            endorsement.Property(entity => entity.Id).ValueGeneratedOnAdd();
            endorsement.Property(entity => entity.UserId).IsRequired();
            endorsement.Property(entity => entity.Quote).HasMaxLength(1000).IsRequired();
            endorsement.Property(entity => entity.AuthorName).HasMaxLength(120).IsRequired();
            endorsement.Property(entity => entity.AuthorRole).HasMaxLength(120).IsRequired();
            endorsement.Property(entity => entity.AuthorAvatarSeed).HasMaxLength(120).IsRequired();
            endorsement.HasIndex(entity => entity.UserId);
        });
    }
}
