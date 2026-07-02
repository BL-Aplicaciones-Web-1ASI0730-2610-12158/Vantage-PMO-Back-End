using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Meetings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class MeetingsModelBuilderExtensions
{
    public static void ApplyMeetingsConfiguration(this ModelBuilder builder)
    {
        var attendeesConverter = new ValueConverter<List<string>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<string>>(value, (JsonSerializerOptions?)null) ?? new List<string>());

        var attendeesComparer = new ValueComparer<List<string>>(
            (left, right) => (left ?? new List<string>()).SequenceEqual(right ?? new List<string>()),
            value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
            value => value.ToList());

        var minutesConverter = new ValueConverter<List<MeetingMinuteItem>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<MeetingMinuteItem>>(value, (JsonSerializerOptions?)null)
                     ?? new List<MeetingMinuteItem>());

        var minutesComparer = new ValueComparer<List<MeetingMinuteItem>>(
            (left, right) => JsonSerializer.Serialize(left, (JsonSerializerOptions?)null)
                             == JsonSerializer.Serialize(right, (JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<List<MeetingMinuteItem>>(
                JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                (JsonSerializerOptions?)null) ?? new List<MeetingMinuteItem>());

        var agreementsConverter = new ValueConverter<List<MeetingAgreementItem>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<MeetingAgreementItem>>(value, (JsonSerializerOptions?)null)
                       ?? new List<MeetingAgreementItem>());

        var agreementsComparer = new ValueComparer<List<MeetingAgreementItem>>(
            (left, right) => JsonSerializer.Serialize(left, (JsonSerializerOptions?)null)
                             == JsonSerializer.Serialize(right, (JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<List<MeetingAgreementItem>>(
                JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                (JsonSerializerOptions?)null) ?? new List<MeetingAgreementItem>());

        builder.Entity<Meeting>(entity =>
        {
            entity.ToTable("meetings");
            entity.HasKey(meeting => meeting.Id);
            entity.Property(meeting => meeting.Id).ValueGeneratedOnAdd();
            entity.Property(meeting => meeting.Title).IsRequired().HasMaxLength(150);
            entity.Property(meeting => meeting.Date).IsRequired();
            entity.Property(meeting => meeting.Time).IsRequired();
            entity.Property(meeting => meeting.Duration).IsRequired();
            entity.Property(meeting => meeting.Location).IsRequired().HasMaxLength(150);
            entity.Property(meeting => meeting.Type).IsRequired().HasMaxLength(50);
            entity.Property(meeting => meeting.Status).IsRequired().HasMaxLength(50);
            entity.Property(meeting => meeting.Organizer).IsRequired().HasMaxLength(150);
            entity.Property(meeting => meeting.Description).HasMaxLength(2000);
            entity.Property(meeting => meeting.Segment).HasMaxLength(100);
            entity.Property(meeting => meeting.Attendees)
                .HasConversion(attendeesConverter)
                .Metadata.SetValueComparer(attendeesComparer);
            entity.Property(meeting => meeting.Minutes)
                .HasConversion(minutesConverter)
                .Metadata.SetValueComparer(minutesComparer);
            entity.Property(meeting => meeting.Agreements)
                .HasConversion(agreementsConverter)
                .Metadata.SetValueComparer(agreementsComparer);
        });
    }
}
