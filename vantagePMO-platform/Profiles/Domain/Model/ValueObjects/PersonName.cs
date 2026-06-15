namespace VantagePMO_platform.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing the full display name of a profile owner.
/// </summary>
public sealed record PersonName
{
    /// <summary>Maximum allowed length for a person name.</summary>
    public const int MaxLength = 120;

    /// <summary>
    ///     Initializes a new <see cref="PersonName" /> after validating the supplied value.
    /// </summary>
    /// <param name="fullName">The full name (e.g. "Alex Sterling").</param>
    /// <exception cref="ArgumentException">Thrown when the value is empty or too long.</exception>
    public PersonName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(fullName));

        var trimmed = fullName.Trim();
        if (trimmed.Length > MaxLength)
            throw new ArgumentException($"Name cannot exceed {MaxLength} characters.", nameof(fullName));

        FullName = trimmed;
    }

    /// <summary>Gets the validated full name.</summary>
    public string FullName { get; }

    /// <summary>Gets the first token of the full name.</summary>
    public string FirstName => FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];

    /// <inheritdoc />
    public override string ToString() => FullName;
}
