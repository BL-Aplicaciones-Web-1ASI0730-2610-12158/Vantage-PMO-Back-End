namespace vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

public record ConvertAgreementResultResource(
    MeetingResource Meeting,
    int TaskId,
    string TaskRef);
