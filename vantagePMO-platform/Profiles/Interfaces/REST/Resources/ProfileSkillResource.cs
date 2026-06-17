namespace vantagePMO_platform.Profiles.Interfaces.REST.Resources;

public record ProfileSkillResource(
    int Id,
    int UserId,
    string Name,
    int Percentage);
