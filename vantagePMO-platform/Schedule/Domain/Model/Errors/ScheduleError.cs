namespace vantagePMO_platform.Schedule.Domain.Model.Errors;

/// <summary>
///     Error codes specific to the Schedule bounded context.
/// </summary>
public enum ScheduleError
{
    ScheduleNotFound = 1,
    InvalidScheduleData = 2,
    InvalidDate = 3,
    InvalidTime = 4,
    InvalidDuration = 5,
    InvalidType = 6,
    ScheduleAlreadyExists = 7,
    UnauthorizedScheduleAccess = 8
}
