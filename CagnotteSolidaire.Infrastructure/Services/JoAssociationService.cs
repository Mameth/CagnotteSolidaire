using System.Net.Http.Json;
using CagnotteSolidaire.Domain.Services;
using CagnotteSolidaire.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CagnotteSolidaire.Infrastructure.Services;

public class OpenDataResponse
{
    public int total_count { get; set; }
    public List<OpenDataRecord> results { get; set; } = new();
}

public class OpenDataRecord
{
    public string titre { get; set; } = "";
    public string id_association { get; set; } = "";
    public string objet { get; set; } = "";
    public string adresse_siege { get; set; } = "";
    public string adresse_gestion_code_postal { get; set; } = "";
    public string adresse_gestion_libelle_commune { get; set; } = "";
}

public class JoAssociationService : IJoAssociationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<JoAssociationService> _logger;

    public JoAssociationService(HttpClient httpClient, ILogger<JoAssociationService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Association>> Rechercher(string terme, string departement)
    {
        try
        {
            var query = $"q={terme} AND adresse_gestion_code_postal LIKE '68*'";
            var url = $"/api/explore/v2.1/catalog/datasets/jo_associations/records?limit=20&{query}";

            _logger.LogInformation($"URL: {url}");

            var response = await _httpClient.GetFromJsonAsync<OpenDataResponse>(url);

            if (response == null || response.results == null)
            {
                return new List<Association>();
            }

            return response.results.Select(r => new Association(
                Guid.NewGuid(),
                r.titre,
                r.id_association,
                r.adresse_gestion_code_postal,
                r.objet
            )).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erreur: {ex.Message}");
            return new List<Association>();
        }
    }
}