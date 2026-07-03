using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vantagePMO_platform.Projects.Domain.Model.Aggregates;
using vantagePMO_platform.Projects.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Projects.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProjectsConfiguration(this ModelBuilder builder)
    {
        var teamMemberConverter = new ValueConverter<List<TeamMember>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<TeamMember>>(value, (JsonSerializerOptions?)null) ?? new List<TeamMember>());

        var teamMemberComparer = new ValueComparer<List<TeamMember>>(
            (left, right) => JsonSerializer.Serialize(left ?? new List<TeamMember>(), (JsonSerializerOptions?)null)
                             == JsonSerializer.Serialize(right ?? new List<TeamMember>(), (JsonSerializerOptions?)null),
            value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.Id.GetHashCode(), item.Name.GetHashCode())),
            value => JsonSerializer.Deserialize<List<TeamMember>>(
                         JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                         (JsonSerializerOptions?)null) ?? new List<TeamMember>());

        var milestoneConverter = new ValueConverter<List<Milestone>, string>(
            value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
            value => JsonSerializer.Deserialize<List<Milestone>>(value, (JsonSerializerOptions?)null) ?? new List<Milestone>());

        var milestoneComparer = new ValueComparer<List<Milestone>>(
            (left, right) => JsonSerializer.Serialize(left ?? new List<Milestone>(), (JsonSerializerOptions?)null)
                             == JsonSerializer.Serialize(right ?? new List<Milestone>(), (JsonSerializerOptions?)null),
            value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.Id.GetHashCode(), item.Name.GetHashCode())),
            value => JsonSerializer.Deserialize<List<Milestone>>(
                         JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                         (JsonSerializerOptions?)null) ?? new List<Milestone>());

        builder.Entity<Project>(project =>
        {
            project.HasKey(p => p.Id);
            project.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            project.Property(p => p.Name).HasMaxLength(200).IsRequired();
            project.Property(p => p.Category).HasMaxLength(120);
            project.Property(p => p.Description).HasMaxLength(2000);
            project.Property(p => p.Status).HasMaxLength(60);
            project.Property(p => p.StartDate).HasMaxLength(40);
            project.Property(p => p.EndDate).HasMaxLength(40);
            project.Property(p => p.DueDate).HasMaxLength(80);
            project.Property(p => p.Manager).HasMaxLength(120);
            project.Property(p => p.UserId);

            project.Property(p => p.TeamMembers)
                .HasConversion(teamMemberConverter)
                .Metadata.SetValueComparer(teamMemberComparer);

            project.Property(p => p.Milestones)
                .HasConversion(milestoneConverter)
                .Metadata.SetValueComparer(milestoneComparer);
        });
    }
}
