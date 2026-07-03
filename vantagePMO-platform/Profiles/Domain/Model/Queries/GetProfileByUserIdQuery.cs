namespace vantagePMO_platform.Profiles.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a profile by its linked IAM user identifier.
/// </summary>
public record GetProfileByUserIdQuery(int UserId);
