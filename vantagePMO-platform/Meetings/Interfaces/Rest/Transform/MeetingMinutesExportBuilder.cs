using System.Text;
using vantagePMO_platform.Meetings.Domain.Model.Aggregates;

namespace vantagePMO_platform.Meetings.Interfaces.Rest.Transform;

public static class MeetingMinutesExportBuilder
{
    public static string BuildCsv(Meeting meeting, bool includeAttendees, bool includeAgenda,
        bool includeMinutes, bool includeAgreements)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Meeting,{Escape(meeting.Title)}");
        builder.AppendLine($"Segment,{Escape(meeting.Segment)}");
        builder.AppendLine($"Date,{meeting.Date:dd-MM-yyyy}");
        builder.AppendLine($"Time,{meeting.Time:HH:mm}");
        builder.AppendLine($"Status,{Escape(meeting.Status)}");
        builder.AppendLine();

        if (includeAttendees)
        {
            builder.AppendLine("Attendees");
            foreach (var attendee in meeting.Attendees)
                builder.AppendLine(Escape(attendee));
            builder.AppendLine();
        }

        if (includeAgenda)
        {
            builder.AppendLine("Agenda");
            builder.AppendLine(Escape(meeting.Description));
            builder.AppendLine();
        }

        if (includeMinutes)
        {
            builder.AppendLine("Time,Title,Body");
            foreach (var minute in meeting.Minutes)
            {
                builder.AppendLine(
                    $"{Escape(minute.Time)},{Escape(minute.Title)},{Escape(minute.Body)}");
            }
            builder.AppendLine();
        }

        if (includeAgreements)
        {
            builder.AppendLine("Agreement,Owner,Deadline,Status,TaskRef");
            foreach (var agreement in meeting.Agreements)
            {
                builder.AppendLine(string.Join(',',
                    Escape(agreement.Title),
                    Escape(agreement.Owner),
                    Escape(agreement.Deadline ?? string.Empty),
                    Escape(agreement.Status ?? string.Empty),
                    Escape(agreement.TaskRef ?? string.Empty)));
            }
        }

        return builder.ToString();
    }

    private static string Escape(string value) =>
        $"\"{value.Replace("\"", "\"\"", StringComparison.Ordinal)}\"";
}
