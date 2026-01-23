using System.ComponentModel.DataAnnotations;

namespace CagnotteSolidaire.Blazor.Models;

public class LoginModel
{
    [Required(ErrorMessage = "L'email est obligatoire")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    public string Password { get; set; } = string.Empty;
}