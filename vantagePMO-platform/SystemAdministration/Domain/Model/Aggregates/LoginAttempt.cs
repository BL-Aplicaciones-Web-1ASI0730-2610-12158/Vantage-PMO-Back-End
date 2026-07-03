namespace vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

public class LoginAttempt
{
    protected LoginAttempt()
    {
        User = string.Empty;
        Timestamp = string.Empty;
        Status = string.Empty;
    }

    public LoginAttempt(string user, string timestamp, string status)
    {
        User = user;
        Timestamp = timestamp;
        Status = status;
    }

    public int Id { get; private set; }
    public string User { get; private set; }
    public string Timestamp { get; private set; }
    public string Status { get; private set; }
}
