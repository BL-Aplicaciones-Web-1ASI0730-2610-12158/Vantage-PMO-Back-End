using System.Text.RegularExpressions;

namespace VantagePMO_platform.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a syntactically valid email address.
/// </summary>
public sealed partial record EmailAddress
{
    /// <summary>Maximum allowed length for an email address.</summary>
    public const int MaxLength = 160;

    /// <summary>
    ///     Initializes a new <see cref="EmailAddress" /> after validating the supplied value.
    /// </summary>
    /// <param name="value">The email address.</param>
    /// <exception cref="ArgumentException">Thrown when the value is empty, too long, or malformed.</exception>
    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be null or whitespace.", nameof(value));

        var normalized = value.Trim();
        if (normalized.Length > MaxLength)
            throw new ArgumentException($"Email cannot exceed {MaxLength} characters.", nameof(value));

        if (!EmailRegex().IsMatch(normalized))
            throw new ArgumentException("Email format is invalid.", nameof(value));

        Value = normalized;
    }

    /// <summary>Gets the validated email address.</summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled)]
    private static partial Regex EmailRegex();
}
