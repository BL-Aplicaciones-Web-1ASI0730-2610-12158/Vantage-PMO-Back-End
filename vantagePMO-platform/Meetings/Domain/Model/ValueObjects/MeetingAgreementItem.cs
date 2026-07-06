namespace vantagePMO_platform.Meetings.Domain.Model.ValueObjects;

public class MeetingAgreementItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string? Deadline { get; set; }
    public string? Tag { get; set; }
    public string? Status { get; set; }
    public string? Verified { get; set; }
    public string? Note { get; set; }
    public string? TaskRef { get; set; }
}
