using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.Queries;

namespace vantagePMO_platform.Meetings.Application.QueryServices;

/// <summary>
/// Contrato del servicio de consultas de Meeting.
/// Implementado en Application/Internal/QueryServices/MeetingQueryService.
/// </summary>
public interface IMeetingQueryService
{
    /// <summary>Devuelve todas las reuniones registradas.</summary>
    Task<IEnumerable<Meeting>> Handle(GetAllMeetingsQuery query);
}
