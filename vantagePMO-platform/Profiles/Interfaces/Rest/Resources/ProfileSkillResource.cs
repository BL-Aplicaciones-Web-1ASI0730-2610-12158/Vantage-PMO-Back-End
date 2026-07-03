namespace vantagePMO_platform.Profiles.Interfaces.Rest.Resources;

public record ProfileSkillResource(
    int Id,
    int UserId,
    string Name,
    int Percentage);
