namespace CagnotteSolidaire.Domain.ValueObjects;


public sealed class Money : IEquatable<Money>
{
    public decimal Value { get; }

    public Money(decimal value)
    {
        if (value <= 0)
        throw new ArgumentOutOfRangeException(
            nameof(value),
            value,
            "Le montant doit être strictement supérieur à zéro."
        );

        Value = value;
    }

    public bool Equals(Money? other)
        => other != null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as Money);

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString("0.00");
}
