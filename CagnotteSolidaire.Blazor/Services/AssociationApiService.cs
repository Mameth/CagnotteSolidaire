using System.Net.Http.Json;
namespace CagnotteSolidaire.Blazor.Services;
public class AssociationApiService
{
    private readonly HttpClient _http;

    public AssociationApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<AssociationDTO>> Rechercher(string terme)
    {
        return await _http.GetFromJsonAsync<List<AssociationDTO>>(
            $"api/associations/recherche?q={terme}"
        ) ?? [];
    }
}
