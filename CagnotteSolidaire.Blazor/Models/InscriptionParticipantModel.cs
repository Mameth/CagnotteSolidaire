using System.ComponentModel.DataAnnotations;

namespace CagnotteSolidaire.Blazor.Models;

public class InscriptionParticipantModel
{
    [Required(ErrorMessage = "Le nom est requis")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le prénom est requis")]
    public string Prenom { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est requis")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit faire au moins 6 caractères")]
    public string MotDePasse { get; set; } = string.Empty;
}