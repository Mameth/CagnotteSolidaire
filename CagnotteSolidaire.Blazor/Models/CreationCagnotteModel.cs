using System.ComponentModel.DataAnnotations;

namespace CagnotteSolidaire.Blazor.Models;

public class CreationCagnotteModel
{
    [Required(ErrorMessage = "Le titre est obligatoire")]
    public string Titre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description est obligatoire")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'objectif financier est requis")]
    [Range(1, 1000000, ErrorMessage = "Le montant doit être positif")]
    public decimal Objectif { get; set; }
}