using System.Net.Http.Json;
using System.Net.Http.Headers; 
using CagnotteSolidaire.Blazor.Models;
using Blazored.LocalStorage; 

namespace CagnotteSolidaire.Blazor.Services;

public class CagnotteApiService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public CagnotteApiService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    private async Task AjouterTokenJwt()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            _http.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    // --- Méthodes existantes (Pas touchées) ---

    public async Task<List<CagnotteDTO>> GetMesCagnottes()
    {
        await AjouterTokenJwt(); 
        var result = await _http.GetFromJsonAsync<List<CagnotteDTO>>("api/cagnottes");
        return result ?? new List<CagnotteDTO>();
    }

    public async Task CreerCagnotte(CreationCagnotteModel model)
    {
        await AjouterTokenJwt(); 
        var payload = new 
        {
            Nom = model.Titre,
            Description = model.Description,
            Objectif = model.Objectif.ToString()

        };

        var response = await _http.PostAsJsonAsync("api/cagnottes", payload);

        if (!response.IsSuccessStatusCode)
        {
             var error = await response.Content.ReadAsStringAsync();
             throw new Exception(error);
        }
    }

    // --- 👇 NOUVELLES MÉTHODES AJOUTÉES (Nécessaires pour le Donateur) 👇 ---

    // 1. Récupérer le détail d'une cagnotte
    public async Task<CagnotteDTO?> GetCagnotteById(Guid id)
    {
        await AjouterTokenJwt(); // On met le token au cas où, même si c'est public
        try
        {
            // Appelle la route GET api/cagnottes/{id}
            return await _http.GetFromJsonAsync<CagnotteDTO>($"api/cagnottes/{id}");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null; // Si pas trouvé, on renvoie null proprement
        }
    }

    // 2. Envoyer une participation (Faire un don)
    public async Task Participer(Guid cagnotteId, decimal montant)
    {
        await AjouterTokenJwt(); // Indispensable : il faut être connecté pour donner

        // Le payload correspond à "ParticipationRequest" de l'API (Montant)
        var payload = new { Montant = montant };
        
        // Appelle la route POST api/cagnottes/{id}/participer
        var response = await _http.PostAsJsonAsync($"api/cagnottes/{cagnotteId}/participer", payload);

        if (!response.IsSuccessStatusCode)
        {
             var error = await response.Content.ReadAsStringAsync();
             throw new Exception(error);
        }
    }
}