namespace vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;

public class Branding
{
    public const int SingletonId = 1;

    protected Branding()
    {
        CompanyName = string.Empty;
        CompanyDescription = string.Empty;
        LogoUrl = string.Empty;
        PrimaryColor = string.Empty;
        SecondaryColor = string.Empty;
        TypographyStyle = string.Empty;
        CreatedAt = string.Empty;
        UpdatedAt = string.Empty;
    }

    public Branding(
        string companyName,
        string? companyDescription,
        string logoUrl,
        string primaryColor,
        string secondaryColor,
        string? typographyStyle,
        string createdAt,
        string updatedAt)
    {
        Id = SingletonId;
        CompanyName = companyName;
        CompanyDescription = companyDescription ?? string.Empty;
        LogoUrl = logoUrl;
        PrimaryColor = primaryColor;
        SecondaryColor = secondaryColor;
        TypographyStyle = typographyStyle ?? "Source Sans Pro";
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public int Id { get; private set; }
    public string CompanyName { get; private set; }
    public string CompanyDescription { get; private set; }
    public string LogoUrl { get; private set; }
    public string PrimaryColor { get; private set; }
    public string SecondaryColor { get; private set; }
    public string TypographyStyle { get; private set; }
    public string CreatedAt { get; private set; }
    public string UpdatedAt { get; private set; }

    public void Update(
        string companyName,
        string? companyDescription,
        string logoUrl,
        string primaryColor,
        string secondaryColor,
        string? typographyStyle,
        string updatedAt)
    {
        CompanyName = companyName;
        CompanyDescription = companyDescription ?? string.Empty;
        LogoUrl = logoUrl;
        PrimaryColor = primaryColor;
        SecondaryColor = secondaryColor;
        TypographyStyle = typographyStyle ?? TypographyStyle;
        UpdatedAt = updatedAt;
    }
}
