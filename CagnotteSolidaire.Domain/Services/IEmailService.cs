namespace CagnotteSolidaire.Domain.Services;

public interface IEmailService
{
    Task EnvoyerEmail(string destinataire, string sujet, string corps);
}