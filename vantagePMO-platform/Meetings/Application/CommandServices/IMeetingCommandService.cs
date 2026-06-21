using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Model.Commands;

namespace vantagePMO_platform.Meetings.Application.CommandServices;

/// <summary>
/// Contrato del servicio de comandos de Meeting.
/// Implementado en Application/Internal/CommandServices/MeetingCommandService.
/// </summary>
public interface IMeetingCommandService
{
    /// <summary>Crea una nueva reunión y devuelve la entidad creada (o null si falla).</summary>
    Task<Meeting?> Handle(CreateMeetingCommand command);
}
