using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using vantagePMO_platform.Meetings.Domain.Model.Aggregates;

namespace vantagePMO_platform.Meetings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
/// Configuración de mapeo EF Core para el agregado Meeting.
/// Llamar a ApplyMeetingsConfiguration(builder) desde
/// AppDbContext.OnModelCreating, junto con la configuración de los demás contextos.
/// </summary>
public static class MeetingsModelBuilderExtensions
{
    public static void ApplyMeetingsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Meeting>(entity =>
        {
            entity.ToTable("meetings");

            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(m => m.Title).IsRequired().HasMaxLength(150);
            entity.Property(m => m.Date).IsRequired();
            entity.Property(m => m.Time).IsRequired();
            entity.Property(m => m.Duration).IsRequired().HasMaxLength(50);
            entity.Property(m => m.Location).IsRequired().HasMaxLength(150);
            entity.Property(m => m.Type).IsRequired().HasMaxLength(50);
            entity.Property(m => m.Status).IsRequired().HasMaxLength(50);
            entity.Property(m => m.Organizer).IsRequired().HasMaxLength(150);
            entity.Property(m => m.Description).HasMaxLength(1000);
            entity.Property(m => m.Segment).HasMaxLength(100);

            // Las listas (attendees, minutes, agreements) se serializan como JSON
            // en una sola columna de texto, ya que son colecciones de valores simples.
            entity.Property(m => m.Attendees)
                .HasConversion(
                    list => JsonSerializer.Serialize(list, (JsonSerializerOptions?)null),
                    json => JsonSerializer.Deserialize<List<string>>(json, (JsonSerializerOptions?)null) ?? new List<string>())
                .Metadata.SetValueComparer(StringListComparer);

            entity.Property(m => m.Minutes)
                .HasConversion(
                    list => JsonSerializer.Serialize(list, (JsonSerializerOptions?)null),
                    json => JsonSerializer.Deserialize<List<string>>(json, (JsonSerializerOptions?)null) ?? new List<string>())
                .Metadata.SetValueComparer(StringListComparer);

            entity.Property(m => m.Agreements)
                .HasConversion(
                    list => JsonSerializer.Serialize(list, (JsonSerializerOptions?)null),
                    json => JsonSerializer.Deserialize<List<string>>(json, (JsonSerializerOptions?)null) ?? new List<string>())
                .Metadata.SetValueComparer(StringListComparer);
        });
    }

    private static readonly ValueComparer<List<string>> StringListComparer = new(
        (a, b) => (a ?? new List<string>()).SequenceEqual(b ?? new List<string>()),
        v => v.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
        v => v.ToList());
}
