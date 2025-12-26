using CagnotteSolidaire.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace CagnotteSolidaire.Domain.ValueObjects;

public sealed class Email
{
    public string Valeur { get; }

    private Email(string valeur)
    {
        Valeur = valeur;
    }

    public static Email Creer(string valeur)
    {
        if (string.IsNullOrWhiteSpace(valeur))
            throw new BusinessException("L'email est obligatoire.");

        if (!Regex.IsMatch(valeur, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new BusinessException("Format d'email invalide.");

        return new Email(valeur);
    }

    public override string ToString() => Valeur;

    public override bool Equals(object? obj)
        => obj is Email other && Valeur == other.Valeur;

    public override int GetHashCode()
        => Valeur.GetHashCode();
}
