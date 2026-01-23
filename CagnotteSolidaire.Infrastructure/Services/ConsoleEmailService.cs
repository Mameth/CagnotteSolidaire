using CagnotteSolidaire.Domain.Services;

namespace CagnotteSolidaire.Infrastructure.Services;

public class ConsoleEmailService : IEmailService
{
    public Task EnvoyerEmail(string destinataire, string sujet, string corps)
    {
        // Simulation : on écrit juste dans la console du serveur
        Console.WriteLine($" [SIMULATION EMAIL] À: {destinataire} | Sujet: {sujet}");
        Console.WriteLine($"   Corps: {corps}");
        return Task.CompletedTask;
    }
}