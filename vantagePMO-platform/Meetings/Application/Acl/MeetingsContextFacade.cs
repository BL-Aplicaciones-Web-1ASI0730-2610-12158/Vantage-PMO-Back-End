using vantagePMO_platform.Meetings.Application.CommandServices;
using vantagePMO_platform.Meetings.Domain.Model.Commands;
using vantagePMO_platform.Meetings.Domain.Repositories;
using vantagePMO_platform.Meetings.Interfaces.Acl;

namespace vantagePMO_platform.Meetings.Application.Acl;

/// <summary>
/// Implementación de IMeetingsContextFacade. Es el único punto de entrada
/// que otros bounded contexts deberían usar para interactuar con Meetings.
/// </summary>
public class MeetingsContextFacade(
    IMeetingCommandService meetingCommandService,
    IMeetingRepository meetingRepository) : IMeetingsContextFacade
{
    public async Task<int> CreateMeeting(string title, DateOnly date, TimeOnly time, string organizer)
    {
        var command = new CreateMeetingCommand(
            title,
            date,
            time,
            Duration: 60,
            Location: string.Empty,
            Type: string.Empty,
            Status: "Scheduled",
            Organizer: organizer,
            Attendees: null,
            Description: null,
            Minutes: null,
            Agreements: null,
            Segment: null);

        var meeting = await meetingCommandService.Handle(command);
        return meeting?.Id ?? 0;
    }

    public async Task<bool> MeetingExists(int meetingId)
    {
        var meeting = await meetingRepository.FindByIdAsync(meetingId);
        return meeting is not null;
    }
}
