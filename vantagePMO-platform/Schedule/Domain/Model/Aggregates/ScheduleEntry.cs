using vantagePMO_platform.Schedule.Domain.Model.Commands;
using vantagePMO_platform.Shared.Domain.Model.Entities;

namespace vantagePMO_platform.Schedule.Domain.Model.Aggregates;

/// <summary>
///     Schedule aggregate root for the Schedule bounded context.
/// </summary>
public class ScheduleEntry : IAuditableEntity
{
    /// <summary>
    ///     Required by EF Core
    /// </summary>
    protected ScheduleEntry()
    {
        Title = string.Empty;
        Detail = string.Empty;
        Type = string.Empty;
    }

    public ScheduleEntry(CreateScheduleCommand command)
    {
        if (command.UserId <= 0)
            throw new ArgumentException("UserId must be greater than zero.", nameof(command));

        if (command.Date == default)
            throw new ArgumentException("Date cannot be default.", nameof(command));

        if (string.IsNullOrWhiteSpace(command.Title))
            throw new ArgumentException("Title cannot be empty.", nameof(command));

        UserId = command.UserId;
        Date = command.Date;
        Time = command.Time;
        Duration = command.Duration > 0 ? command.Duration : 60;
        Title = command.Title;
        Detail = command.Detail ?? string.Empty;
        Type = command.Type ?? "work";
        Active = command.Active;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public TimeOnly Time { get; set; }
    public int Duration { get; set; } = 60;
    public string Title { get; set; } = null!;
    public string Detail { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool Active { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Update(UpdateScheduleCommand command)
    {
        if (command.Date.HasValue)
            Date = command.Date.Value;

        if (command.Time.HasValue)
            Time = command.Time.Value;

        if (command.Duration.HasValue && command.Duration > 0)
            Duration = command.Duration.Value;

        if (!string.IsNullOrWhiteSpace(command.Title))
            Title = command.Title;

        if (command.Detail != null)
            Detail = command.Detail;

        if (!string.IsNullOrWhiteSpace(command.Type))
            Type = command.Type;

        if (command.Active.HasValue)
            Active = command.Active.Value;

        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
