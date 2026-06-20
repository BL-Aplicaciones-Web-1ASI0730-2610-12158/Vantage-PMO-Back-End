namespace vantagePMO_platform.ChatHub.Domain.Model.ValueObjects;

public class InsightItem
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
