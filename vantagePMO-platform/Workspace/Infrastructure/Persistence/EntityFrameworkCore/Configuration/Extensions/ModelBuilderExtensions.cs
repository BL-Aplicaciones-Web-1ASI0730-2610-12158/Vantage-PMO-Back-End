using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Workspace.Domain.Model.Aggregates;

namespace vantagePMO_platform.Workspace.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyWorkspaceConfiguration(this ModelBuilder builder)
    {
        builder.Entity<WorkspaceSelection>(selection =>
        {
            selection.ToTable("workspace_selections");
            selection.HasKey(entity => entity.Id);
            selection.Property(entity => entity.Id).ValueGeneratedOnAdd();
            selection.Property(entity => entity.UserId).IsRequired();
            selection.Property(entity => entity.SelectedRole).HasMaxLength(64).IsRequired();
            selection.HasIndex(entity => entity.UserId).IsUnique();
        });
    }
}
