namespace vantagePMO_platform.Meetings.Interfaces.Acl;

/// <summary>
/// Anti-Corruption Layer: contrato que expone el bounded context Meetings
/// hacia otros bounded contexts (por ejemplo Projects o Notifications),
/// sin que estos dependan directamente de las entidades internas de Meetings.
/// Implementado en Application/Acl/MeetingsContextFacade.
/// </summary>
public interface IMeetingsContextFacade
{
    /// <summary>
    /// Crea una reunión mínima desde otro bounded context y devuelve su Id (0 si falla).
    /// </summary>
    Task<int> CreateMeeting(string title, DateOnly date, TimeOnly time, string organizer);

    /// <summary>Indica si existe una reunión con el Id indicado.</summary>
    Task<bool> MeetingExists(int meetingId);
}
