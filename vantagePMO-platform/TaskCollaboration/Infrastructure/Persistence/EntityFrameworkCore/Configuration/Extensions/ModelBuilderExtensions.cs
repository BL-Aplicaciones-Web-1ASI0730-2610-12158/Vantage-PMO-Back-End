using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;

namespace vantagePMO_platform.TaskCollaboration.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyTaskCollaborationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Board>(board =>
        {
            board.ToTable("boards");
            board.HasKey(entity => entity.Id);
            board.Property(entity => entity.Id).ValueGeneratedOnAdd();
            board.Property(entity => entity.Name).HasMaxLength(200).IsRequired();
            board.Property(entity => entity.Description).HasMaxLength(500);
        });

        builder.Entity<BoardMember>(member =>
        {
            member.ToTable("board_members");
            member.HasKey(entity => entity.Id);
            member.Property(entity => entity.Id).ValueGeneratedOnAdd();
            member.Property(entity => entity.Name).HasMaxLength(120).IsRequired();
            member.Property(entity => entity.Role).HasMaxLength(120);
            member.Property(entity => entity.Avatar).HasMaxLength(10);
            member.Property(entity => entity.Color).HasMaxLength(20);
            member.Property(entity => entity.Status).HasMaxLength(20);
        });

        builder.Entity<CollaborationTask>(task =>
        {
            task.ToTable("collaboration_tasks");
            task.HasKey(entity => entity.Id);
            task.Property(entity => entity.Id).ValueGeneratedOnAdd();
            task.Property(entity => entity.Project).HasMaxLength(120);
            task.Property(entity => entity.Title).HasMaxLength(300).IsRequired();
            task.Property(entity => entity.Description).HasMaxLength(2000);
            task.Property(entity => entity.Assignee).HasMaxLength(120);
            task.Property(entity => entity.AssigneeAvatar).HasMaxLength(10);
            task.Property(entity => entity.AssigneeColor).HasMaxLength(20);
            task.Property(entity => entity.Status).HasMaxLength(40);
            task.Property(entity => entity.Priority).HasMaxLength(40);
            task.Property(entity => entity.PriorityColor).HasMaxLength(20);
            task.Property(entity => entity.DueDate).HasMaxLength(20);
            task.Property(entity => entity.Department).HasMaxLength(120);
        });
    }
}
