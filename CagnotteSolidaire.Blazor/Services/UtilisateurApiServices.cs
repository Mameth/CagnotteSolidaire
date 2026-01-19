using System.Net.Http.Json;
using CagnotteSolidaire.Blazor.Models;

namespace CagnotteSolidaire.Blazor.Services;

public class UtilisateurApiService
{
    private readonly HttpClient _http;

    public UtilisateurApiService(HttpClient http)
    {
        _http = http;
    }

    // 🔹 INSCRIPTION PARTICIPANT
    public async Task InscrireParticipant(InscriptionParticipantModel model)
    {
        var payload = new
        {
            nom = model.Nom,
            prenom = model.Prenom,
            email = model.Email
        };

        var response = await _http.PostAsJsonAsync(
            "api/utilisateurs/participants",
            payload);

        response.EnsureSuccessStatusCode();
    }

    // 🔹 INSCRIPTION GESTIONNAIRE (rappel)
    public async Task InscrireGestionnaire(
        string nom,
        string prenom,
        string email,
        Guid associationId,
        string associationNom,
        string associationRna)
    {
        var payload = new
        {
            nom,
            prenom,
            email,
            associationId,
            associationNom,
            associationRna
        };

        var response = await _http.PostAsJsonAsync(
            "api/utilisateurs/gestionnaires",
            payload);

        response.EnsureSuccessStatusCode();
    }
}
