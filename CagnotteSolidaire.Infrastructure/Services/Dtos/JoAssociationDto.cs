using System.Text.Json.Serialization;

namespace CagnotteSolidaire.Infrastructure.Services.Dtos;

public class JoAssociationDto
{
    [JsonPropertyName("titre")]
    public string Nom { get; set; } = string.Empty;

    [JsonPropertyName("identifiant_rna")]
    public string Rna { get; set; } = string.Empty;

    [JsonPropertyName("departement")]
    public string Departement { get; set; } = string.Empty;
}
