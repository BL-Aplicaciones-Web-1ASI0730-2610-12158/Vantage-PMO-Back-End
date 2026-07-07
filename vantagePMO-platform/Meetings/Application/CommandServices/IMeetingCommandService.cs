using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.Commands;

namespace vantagePMO_platform.Meetings.Application.CommandServices;
public interface IMeetingCommandService
{
    Task<Meeting?> Handle(CreateMeetingCommand command);

    Task<(Meeting? Meeting, int? TaskId, string? TaskRef)> ConvertAgreementToTaskAsync(
        ConvertMeetingAgreementCommand command,
        CancellationToken cancellationToken = default);
}
