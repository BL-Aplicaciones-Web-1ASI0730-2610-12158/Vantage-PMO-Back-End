namespace vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

public class SystemSettings
{
    public const int SingletonId = 1;

    protected SystemSettings()
    {
        CreatedAt = string.Empty;
        UpdatedAt = string.Empty;
    }

    public SystemSettings(
        bool emailNotifications,
        bool pushNotifications,
        bool reportAlerts,
        bool adminAlerts,
        bool projectAlerts,
        bool weeklyDigest,
        bool mentionNotifications,
        bool deadlineReminders,
        bool browserPush,
        bool mobilePush,
        bool soundAlerts,
        bool securityAlerts,
        bool workspaceUpdates,
        bool subscriptionAlerts,
        bool teamActivity,
        string createdAt,
        string updatedAt)
    {
        Id = SingletonId;
        EmailNotifications = emailNotifications;
        PushNotifications = pushNotifications;
        ReportAlerts = reportAlerts;
        AdminAlerts = adminAlerts;
        ProjectAlerts = projectAlerts;
        WeeklyDigest = weeklyDigest;
        MentionNotifications = mentionNotifications;
        DeadlineReminders = deadlineReminders;
        BrowserPush = browserPush;
        MobilePush = mobilePush;
        SoundAlerts = soundAlerts;
        SecurityAlerts = securityAlerts;
        WorkspaceUpdates = workspaceUpdates;
        SubscriptionAlerts = subscriptionAlerts;
        TeamActivity = teamActivity;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int Id { get; private set; }
    public bool EmailNotifications { get; private set; }
    public bool PushNotifications { get; private set; }
    public bool ReportAlerts { get; private set; }
    public bool AdminAlerts { get; private set; }
    public bool ProjectAlerts { get; private set; }
    public bool WeeklyDigest { get; private set; }
    public bool MentionNotifications { get; private set; }
    public bool DeadlineReminders { get; private set; }
    public bool BrowserPush { get; private set; }
    public bool MobilePush { get; private set; }
    public bool SoundAlerts { get; private set; }
    public bool SecurityAlerts { get; private set; }
    public bool WorkspaceUpdates { get; private set; }
    public bool SubscriptionAlerts { get; private set; }
    public bool TeamActivity { get; private set; }
    public string CreatedAt { get; private set; }
    public string UpdatedAt { get; private set; }

    public void Update(
        bool emailNotifications,
        bool pushNotifications,
        bool reportAlerts,
        bool adminAlerts,
        bool projectAlerts,
        bool weeklyDigest,
        bool mentionNotifications,
        bool deadlineReminders,
        bool browserPush,
        bool mobilePush,
        bool soundAlerts,
        bool securityAlerts,
        bool workspaceUpdates,
        bool subscriptionAlerts,
        bool teamActivity,
        string updatedAt)
    {
        EmailNotifications = emailNotifications;
        PushNotifications = pushNotifications;
        ReportAlerts = reportAlerts;
        AdminAlerts = adminAlerts;
        ProjectAlerts = projectAlerts;
        WeeklyDigest = weeklyDigest;
        MentionNotifications = mentionNotifications;
        DeadlineReminders = deadlineReminders;
        BrowserPush = browserPush;
        MobilePush = mobilePush;
        SoundAlerts = soundAlerts;
        SecurityAlerts = securityAlerts;
        WorkspaceUpdates = workspaceUpdates;
        SubscriptionAlerts = subscriptionAlerts;
        TeamActivity = teamActivity;
        UpdatedAt = updatedAt;
    }
}
