using CagnotteSolidaire.Domain.Exceptions;

namespace CagnotteSolidaire.Domain.ValueObjects;

public sealed class Money
{
    public decimal Valeur { get; }

    private Money(decimal valeur)
    {
        Valeur = valeur;
    }

    public static Money Creer(decimal valeur)
    {
        if (valeur < 0)
            throw new BusinessException("Un montant ne peut pas être négatif.");

        return new Money(valeur);
    }

    public static Money operator +(Money a, Money b)
        => new Money(a.Valeur + b.Valeur);

    public override string ToString()
        => Valeur.ToString("0.00");
}
