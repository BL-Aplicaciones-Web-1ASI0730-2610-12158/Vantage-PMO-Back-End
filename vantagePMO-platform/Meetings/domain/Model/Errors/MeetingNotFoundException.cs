namespace vantagePMO_platform.Meetings.Domain.Model.Errors;

/// <summary>
/// Excepción de dominio lanzada cuando no se encuentra una reunión por su Id.
/// Lista para usarse cuando agregues endpoints como getMeetingById o updateMeeting.
/// </summary>
public class MeetingNotFoundException : Exception
{
    public MeetingNotFoundException(int meetingId)
        : base($"No se encontró la reunión con Id '{meetingId}'.")
    {
    }
}
