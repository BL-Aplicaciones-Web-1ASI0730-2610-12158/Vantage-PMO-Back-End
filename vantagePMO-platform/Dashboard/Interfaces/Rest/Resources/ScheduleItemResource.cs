namespace vantagePMO_platform.Dashboard.Interfaces.Rest.Resources;

public record ScheduleItemResource(
    int Id,
    string Date,
    string Time,
    int Duration,
    string Title,
    string Detail,
    string Type,
    bool Active);
