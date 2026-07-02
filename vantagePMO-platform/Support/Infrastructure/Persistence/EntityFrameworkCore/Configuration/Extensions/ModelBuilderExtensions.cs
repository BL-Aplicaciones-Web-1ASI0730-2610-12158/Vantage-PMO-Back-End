using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Support.Domain.Model.Aggregates;

namespace vantagePMO_platform.Support.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySupportConfiguration(this ModelBuilder builder)
    {
        builder.Entity<SupportTicket>(ticket =>
        {
            ticket.ToTable("support_tickets");
            ticket.HasKey(entity => entity.Id);
            ticket.Property(entity => entity.Id).ValueGeneratedOnAdd();
            ticket.Property(entity => entity.Title).HasMaxLength(200).IsRequired();
            ticket.Property(entity => entity.Description).HasMaxLength(2000);
            ticket.Property(entity => entity.Category).HasMaxLength(80).IsRequired();
            ticket.Property(entity => entity.Priority).HasMaxLength(40).IsRequired();
            ticket.Property(entity => entity.Status).HasMaxLength(40).IsRequired();
            ticket.Property(entity => entity.CreatedAt).HasMaxLength(20).IsRequired();
            ticket.Property(entity => entity.Assignee).HasMaxLength(120).IsRequired();
        });
    }
}
