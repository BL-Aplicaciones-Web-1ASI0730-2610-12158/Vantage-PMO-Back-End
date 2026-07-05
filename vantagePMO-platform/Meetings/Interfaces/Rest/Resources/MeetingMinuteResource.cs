namespace vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

public record MeetingMinuteResource(
    int Id, 
    string Time, 
    string Title, 
    string Body);
