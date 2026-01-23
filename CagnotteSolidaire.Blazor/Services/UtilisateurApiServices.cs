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

    public async Task InscrireParticipant(InscriptionParticipantModel model)
    {
        var payload = new
        {
            Nom = model.Nom,
            Prenom = model.Prenom,
            Email = model.Email,
            MotDePasse = model.MotDePasse
        };

        var response = await _http.PostAsJsonAsync("api/utilisateurs/participants", payload);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }

    public async Task InscrireGestionnaire(InscriptionGestionnaireModel model)
    {
        var payload = new
        {
            Nom = model.Nom,
            Prenom = model.Prenom,
            Email = model.Email,
            MotDePasse = model.MotDePasse,

            AssociationId = model.AssociationId!.Value.ToString(),
            AssociationNom = model.AssociationNom,
            AssociationRna = model.AssociationRna
        };

        var response = await _http.PostAsJsonAsync("api/utilisateurs/gestionnaires", payload);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }
}
