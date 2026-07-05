namespace vantagePMO_platform.Meetings.Interfaces.Rest.Resources;

public record MeetingAgreementResource(
    int Id,
    string Title,
    string Owner,
    string? Deadline,
    string? Tag,
    string? Status,
    string? Verified,
    string? Note,
    string? TaskRef);