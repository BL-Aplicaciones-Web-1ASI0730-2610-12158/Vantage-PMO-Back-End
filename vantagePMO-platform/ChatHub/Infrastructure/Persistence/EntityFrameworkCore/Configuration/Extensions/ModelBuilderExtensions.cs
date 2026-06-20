using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;
using vantagePMO_platform.ChatHub.Domain.Model.ValueObjects;

namespace vantagePMO_platform.ChatHub.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyChatHubConfiguration(this ModelBuilder builder)
    {
        var membersConverter = new ValueConverter<List<string>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<string>>(value, (JsonSerializerOptions?)null) ?? new List<string>());

        var membersComparer = new ValueComparer<List<string>>(
            (left, right) => (left ?? new List<string>()).SequenceEqual(right ?? new List<string>()),
            value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
            value => value.ToList());

        var attachmentsConverter = new ValueConverter<List<ChatAttachment>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<ChatAttachment>>(value, (JsonSerializerOptions?)null) ?? new List<ChatAttachment>());

        var attachmentsComparer = new ValueComparer<List<ChatAttachment>>(
            (left, right) => JsonSerializer.Serialize(left, (JsonSerializerOptions?)null)
                             == JsonSerializer.Serialize(right, (JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<List<ChatAttachment>>(
                JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                (JsonSerializerOptions?)null) ?? new List<ChatAttachment>());

        var reactionsConverter = new ValueConverter<List<ChatReaction>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<ChatReaction>>(value, (JsonSerializerOptions?)null) ?? new List<ChatReaction>());

        var reactionsComparer = new ValueComparer<List<ChatReaction>>(
            (left, right) => JsonSerializer.Serialize(left, (JsonSerializerOptions?)null)
                             == JsonSerializer.Serialize(right, (JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<List<ChatReaction>>(
                JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                (JsonSerializerOptions?)null) ?? new List<ChatReaction>());

        var insightsConverter = new ValueConverter<List<InsightItem>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<InsightItem>>(value, (JsonSerializerOptions?)null) ?? new List<InsightItem>());

        var insightsComparer = new ValueComparer<List<InsightItem>>(
            (left, right) => JsonSerializer.Serialize(left, (JsonSerializerOptions?)null)
                             == JsonSerializer.Serialize(right, (JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<List<InsightItem>>(
                JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                (JsonSerializerOptions?)null) ?? new List<InsightItem>());

        builder.Entity<ChatUser>(user =>
        {
            user.ToTable("chat_users");
            user.HasKey(entity => entity.Id);
            user.Property(entity => entity.Id).HasMaxLength(64);
            user.Property(entity => entity.Name).HasMaxLength(120).IsRequired();
            user.Property(entity => entity.AvatarSeed).HasMaxLength(64);
            user.Property(entity => entity.Avatar).HasMaxLength(500);
            user.Property(entity => entity.Status).HasMaxLength(32);
            user.Property(entity => entity.Role).HasMaxLength(120);
        });

        builder.Entity<Chat>(chat =>
        {
            chat.ToTable("chats");
            chat.HasKey(entity => entity.Id);
            chat.Property(entity => entity.Id).HasMaxLength(120);
            chat.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
            chat.Property(entity => entity.Type).HasMaxLength(32).IsRequired();
            chat.Property(entity => entity.Description).HasMaxLength(500);
            chat.Property(entity => entity.Members)
                .HasConversion(membersConverter)
                .Metadata.SetValueComparer(membersComparer);
        });

        builder.Entity<ChatMessage>(message =>
        {
            message.ToTable("chat_messages");
            message.HasKey(entity => entity.Id);
            message.Property(entity => entity.Id).HasMaxLength(64);
            message.Property(entity => entity.ChatId).HasMaxLength(120).IsRequired();
            message.Property(entity => entity.AuthorId).HasMaxLength(64).IsRequired();
            message.Property(entity => entity.Timestamp).HasMaxLength(32);
            message.Property(entity => entity.Text).HasMaxLength(4000);
            message.Property(entity => entity.Attachments)
                .HasConversion(attachmentsConverter)
                .Metadata.SetValueComparer(attachmentsComparer);
            message.Property(entity => entity.Reactions)
                .HasConversion(reactionsConverter)
                .Metadata.SetValueComparer(reactionsComparer);
        });

        builder.Entity<ChatPinnedAsset>(asset =>
        {
            asset.ToTable("chat_pinned_assets");
            asset.HasKey(entity => entity.Id);
            asset.Property(entity => entity.Id).ValueGeneratedOnAdd();
            asset.Property(entity => entity.ChatId).HasMaxLength(120).IsRequired();
            asset.Property(entity => entity.Name).HasMaxLength(300).IsRequired();
            asset.Property(entity => entity.Type).HasMaxLength(32);
            asset.Property(entity => entity.Meta).HasMaxLength(200);
        });

        builder.Entity<ChatInsight>(insight =>
        {
            insight.ToTable("chat_insights");
            insight.HasKey(entity => entity.Id);
            insight.Property(entity => entity.Id).ValueGeneratedOnAdd();
            insight.Property(entity => entity.ChatId).HasMaxLength(120).IsRequired();
            insight.Property(entity => entity.MeetingTag).HasMaxLength(64);
            insight.Property(entity => entity.TimeAgo).HasMaxLength(32);
            insight.Property(entity => entity.MeetingTitle).HasMaxLength(300);
            insight.Property(entity => entity.SentimentText).HasMaxLength(64);
            insight.Property(entity => entity.Insights)
                .HasConversion(insightsConverter)
                .Metadata.SetValueComparer(insightsComparer);
        });
    }
}
