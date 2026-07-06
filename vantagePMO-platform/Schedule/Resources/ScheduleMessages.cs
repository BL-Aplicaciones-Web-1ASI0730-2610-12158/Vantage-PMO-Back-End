namespace vantagePMO_platform.Schedule.Resources;

public static class ScheduleMessages
{
    public const string ScheduleNotFound = "The requested schedule was not found.";
    public const string InvalidScheduleData = "The provided schedule data is invalid.";
    public const string InvalidDate = "The schedule date cannot be in the past.";
    public const string InvalidTime = "The schedule time is invalid.";
    public const string InvalidDuration = "Schedule duration must be greater than zero.";
    public const string InvalidType = "The schedule type is not valid. Valid types are: work, meeting, review, focus, planning, workshop.";
    public const string UnauthorizedAccess = "You do not have permission to access this schedule.";
    public const string ScheduleCreatedSuccessfully = "Schedule created successfully.";
    public const string ScheduleUpdatedSuccessfully = "Schedule updated successfully.";
    public const string ScheduleDeletedSuccessfully = "Schedule deleted successfully.";
}
