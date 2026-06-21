using vantagePMO_platform.Meetings.Application.QueryServices;
using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.Queries;
using vantagePMO_platform.Meetings.Domain.Repositories;

namespace vantagePMO_platform.Meetings.Application.Internal.QueryServices;

/// <summary>
/// Implementación de IMeetingQueryService. Resuelve GetAllMeetingsQuery
/// delegando en IMeetingRepository.
/// </summary>
public class MeetingQueryService(IMeetingRepository meetingRepository) : IMeetingQueryService
{
    public async Task<IEnumerable<Meeting>> Handle(GetAllMeetingsQuery query)
    {
        return await meetingRepository.ListAsync();
    }
}
