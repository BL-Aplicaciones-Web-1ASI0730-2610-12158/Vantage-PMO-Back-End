namespace VantagePMO_platform.Profiles.Domain.Model.Queries;

/// <summary>
///     Query that retrieves a single profile by its email address.
/// </summary>
/// <param name="Email">The email address used to look up the profile.</param>
public record GetProfileByEmailQuery(string Email);
