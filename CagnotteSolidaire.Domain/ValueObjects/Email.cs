namespace CagnotteSolidaire.Domain.ValueObjects;

using System.Text.RegularExpressions;


public sealed class Email : IEquatable<Email>
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string Value { get; }

    public Email(string value)
    {if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Email invalide.", nameof(value));

    value = value.Trim().ToLowerInvariant();

    if (!EmailRegex.IsMatch(value))
        throw new ArgumentException("Email invalide.", nameof(value));
    

        Value = value;
    }

    public bool Equals(Email? other)
        => other != null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as Email);

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value;
}

