using vantagePMO_platform.Meetings.Application.CommandServices;
using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.Commands;
using vantagePMO_platform.Meetings.Domain.Repositories;
using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.TaskCollaboration.Application.CommandServices;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Commands;

namespace vantagePMO_platform.Meetings.Application.Internal.CommandServices;

/// <summary>
/// Implementación de IMeetingCommandService. Orquesta la creación de la entidad
/// Meeting, su persistencia a través de IMeetingRepository y el commit vía IUnitOfWork.
/// </summary>
public class MeetingCommandService(
    IMeetingRepository meetingRepository,
    ICollaborationTaskCommandService collaborationTaskCommandService,
    IUnitOfWork unitOfWork) : IMeetingCommandService
{
    public async Task<Meeting?> Handle(CreateMeetingCommand command)
    {
        var meeting = new Meeting(command);

        await meetingRepository.AddAsync(meeting);
        await unitOfWork.CompleteAsync();

        return meeting;
    }

    public async Task<(Meeting? Meeting, int? TaskId, string? TaskRef)> ConvertAgreementToTaskAsync(
        ConvertMeetingAgreementCommand command,
        CancellationToken cancellationToken = default)
    {
        var meeting = await meetingRepository.FindByIdAsync(command.MeetingId, cancellationToken);
        if (meeting is null)
            return (null, null, null);

        var agreement = meeting.Agreements.FirstOrDefault(item => item.Id == command.AgreementId);
        if (agreement is null ||
            string.Equals(agreement.Status, "converted", StringComparison.OrdinalIgnoreCase))
            return (null, null, null);

        var ownerInitials = string.Concat(
            agreement.Owner
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(part => char.ToUpperInvariant(part[0])));

        if (string.IsNullOrWhiteSpace(ownerInitials))
            ownerInitials = "NA";

        var taskCommand = new CreateCollaborationTaskCommand(
            BoardId: 1,
            Project: meeting.Segment,
            Title: agreement.Title,
            Description: $"Created from meeting \"{meeting.Title}\".",
            Assignee: agreement.Owner,
            AssigneeAvatar: ownerInitials[..Math.Min(ownerInitials.Length, 2)],
            AssigneeColor: "#3b82f6",
            Status: "To Do",
            Priority: string.Equals(agreement.Tag, "Task", StringComparison.OrdinalIgnoreCase)
                ? "MEDIUM"
                : "LOW",
            PriorityColor: "#6b7280",
            Completed: false,
            Comments: 0,
            Attachments: 0,
            DueDate: agreement.Deadline ?? string.Empty,
            Progress: 0,
            Department: meeting.Segment);

        var task = await collaborationTaskCommandService.CreateAsync(taskCommand, cancellationToken);
        if (task is null)
            return (null, null, null);

        if (!meeting.TryConvertAgreementToTask(command.AgreementId, task.Id))
            return (null, null, null);

        meetingRepository.Update(meeting);
        await unitOfWork.CompleteAsync(cancellationToken);

        return (meeting, task.Id, $"#{task.Id}");
    }
}
