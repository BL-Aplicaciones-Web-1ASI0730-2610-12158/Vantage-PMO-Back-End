using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VantagePMO_platform.Profiles.Domain.Model.Aggregates;
using VantagePMO_platform.Profiles.Domain.Model.ValueObjects;

namespace VantagePMO_platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

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

            profile.Property(p => p.Role).HasMaxLength(120);
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
    }
}
