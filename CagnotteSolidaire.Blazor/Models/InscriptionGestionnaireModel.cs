using System.ComponentModel.DataAnnotations;

namespace CagnotteSolidaire.Blazor.Models;

public class InscriptionGestionnaireModel
{
    // Partie infos perso
    [Required(ErrorMessage = "Le nom est requis")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le prénom est requis")]
    public string Prenom { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format invalide")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est requis")]
    [MinLength(6, ErrorMessage = "Minimum 6 caractères")]
    public string MotDePasse { get; set; } = string.Empty;

    // Partie Association (stockée cachée dans le formulaire une fois sélectionnée)
    public Guid? AssociationId { get; set; } // Peut être null au début
    public string AssociationNom { get; set; } = string.Empty;
    public string AssociationRna { get; set; } = string.Empty;
}