namespace CagnotteSolidaire.Blazor.Models;

public class CagnotteDTO
{
    public Guid Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Objectif { get; set; }
    public decimal MontantActuel { get; set; }
    public string Statut { get; set; } = string.Empty;
}