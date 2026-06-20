using vantagePMO_platform.ChatHub.Domain.Model.ValueObjects;

namespace vantagePMO_platform.ChatHub.Domain.Model.Aggregates;

/// <summary>
///     AI-generated insight for a chat channel.
/// </summary>
public class ChatInsight
{
    protected ChatInsight()
    {
        ChatId = string.Empty;
        MeetingTag = string.Empty;
        TimeAgo = string.Empty;
        MeetingTitle = string.Empty;
        SentimentText = string.Empty;
        Insights = new List<InsightItem>();
    }

    public ChatInsight(
        string chatId,
        string meetingTag,
        string timeAgo,
        string meetingTitle,
        IEnumerable<InsightItem>? insights,
        int sentimentProductive,
        string sentimentText)
    {
        ChatId = chatId;
        MeetingTag = meetingTag;
        TimeAgo = timeAgo;
        MeetingTitle = meetingTitle;
        Insights = insights?.ToList() ?? new List<InsightItem>();
        SentimentProductive = sentimentProductive;
        SentimentText = sentimentText;
    }

    public int Id { get; private set; }
    public string ChatId { get; private set; }
    public string MeetingTag { get; private set; }
    public string TimeAgo { get; private set; }
    public string MeetingTitle { get; private set; }
    public List<InsightItem> Insights { get; private set; }
    public int SentimentProductive { get; private set; }
    public string SentimentText { get; private set; }
}
