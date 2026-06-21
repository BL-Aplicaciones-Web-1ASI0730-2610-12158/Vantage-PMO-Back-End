using vantagePMO_platform.Meetings.Application.CommandServices;
using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.Commands;
using vantagePMO_platform.Meetings.Domain.Repositories;
using VantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Meetings.Application.Internal.CommandServices;

/// <summary>
/// Implementación de IMeetingCommandService. Orquesta la creación de la entidad
/// Meeting, su persistencia a través de IMeetingRepository y el commit vía IUnitOfWork.
/// </summary>
public class MeetingCommandService(
    IMeetingRepository meetingRepository,
    IUnitOfWork unitOfWork) : IMeetingCommandService
{
    public async Task<Meeting?> Handle(CreateMeetingCommand command)
    {
        var meeting = new Meeting(command);

        await meetingRepository.AddAsync(meeting);
        await unitOfWork.CompleteAsync();

        return meeting;
    }
}
