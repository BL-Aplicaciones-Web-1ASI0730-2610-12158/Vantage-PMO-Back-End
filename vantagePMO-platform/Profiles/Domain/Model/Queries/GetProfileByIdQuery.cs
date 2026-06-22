namespace vantagePMO_platform.Profiles.Domain.Model.Queries;

/// <summary>
///     Query that retrieves a single profile by its identifier.
/// </summary>
/// <param name="ProfileId">The identifier of the profile to retrieve.</param>
public record GetProfileByIdQuery(int ProfileId);
