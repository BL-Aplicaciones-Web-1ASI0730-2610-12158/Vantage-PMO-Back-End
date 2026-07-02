namespace vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

public class Subscription
{
    public const int SingletonId = 1;

    protected Subscription()
    {
        Plan = string.Empty;
        BillingCycle = string.Empty;
        ExpirationDate = string.Empty;
        Status = string.Empty;
        CreatedAt = string.Empty;
        UpdatedAt = string.Empty;
    }

    public Subscription(
        string plan,
        int activeUsers,
        string billingCycle,
        string expirationDate,
        string status,
        string createdAt,
        string updatedAt)
    {
        Id = SingletonId;
        Plan = plan;
        ActiveUsers = activeUsers;
        BillingCycle = billingCycle;
        ExpirationDate = expirationDate;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int Id { get; private set; }
    public string Plan { get; private set; }
    public int ActiveUsers { get; private set; }
    public string BillingCycle { get; private set; }
    public string ExpirationDate { get; private set; }
    public string Status { get; private set; }
    public string CreatedAt { get; private set; }
    public string UpdatedAt { get; private set; }

    public void Update(
        string plan,
        int activeUsers,
        string billingCycle,
        string expirationDate,
        string status,
        string updatedAt)
    {
        Plan = plan;
        ActiveUsers = activeUsers;
        BillingCycle = billingCycle;
        ExpirationDate = expirationDate;
        Status = status;
        UpdatedAt = updatedAt;
    }

    public void Renew(string updatedAt)
    {
        var expiration = DateOnly.TryParse(ExpirationDate, out var current)
            ? current
            : DateOnly.FromDateTime(DateTime.UtcNow);

        ExpirationDate = BillingCycle.Equals("yearly", StringComparison.OrdinalIgnoreCase)
            ? expiration.AddYears(1).ToString("yyyy-MM-dd")
            : expiration.AddMonths(1).ToString("yyyy-MM-dd");

        Status = "active";
        UpdatedAt = updatedAt;
    }
}
