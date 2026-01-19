using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

using CagnotteSolidaire.Domain.Entities;
using CagnotteSolidaire.Domain.Services;
using CagnotteSolidaire.Infrastructure.Services.Dtos;

namespace CagnotteSolidaire.Infrastructure.Services;

public class JoAssociationService : IJoAssociationService
{
    private readonly HttpClient _http;

    public JoAssociationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<Association>> Rechercher(
    string terme,
    string departement)
{
    var url =
        "/api/explore/v2.1/catalog/datasets/jo_associations/records" +
        $"?q={Uri.EscapeDataString(terme)}" +
        $"&refine=departement:{departement}" +
        "&limit=20";

    Console.WriteLine($"[JO API] URL appelée : {url}");

    var response = await _http.GetAsync(url);

    if (!response.IsSuccessStatusCode)
    {
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"[JO API] Erreur {response.StatusCode} : {error}");
        return [];
    }

    var result = await response.Content.ReadFromJsonAsync<JoApiResponse>();

    if (result == null)
        return [];

    return result.Results
        .Select(dto => new Association(
            Guid.NewGuid(),
            dto.Nom,
            dto.Rna,
            dto.Departement
        ))
        .ToList();
}



}

