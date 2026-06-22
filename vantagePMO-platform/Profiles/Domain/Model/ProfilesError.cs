namespace vantagePMO_platform.Profiles.Domain.Model;

/// <summary>
///     Business errors for the Profiles bounded context.
/// </summary>
/// <remarks>
///     Each member maps to a localized resource key of the form
///     <c>ProfilesError.&lt;Member&gt;</c> in <c>ErrorMessages.resx</c>.
/// </remarks>
public enum ProfilesError
{
    /// <summary>The requested profile does not exist.</summary>
    ProfileNotFound,

    /// <summary>A profile with the given email already exists.</summary>
    EmailAlreadyRegistered,

    /// <summary>The supplied profile data failed domain validation.</summary>
    InvalidProfileData,

    /// <summary>The operation was cancelled by the caller.</summary>
    OperationCancelled,

    /// <summary>A persistence-level error occurred.</summary>
    DatabaseError,

    /// <summary>An unexpected internal error occurred.</summary>
    InternalServerError
}
