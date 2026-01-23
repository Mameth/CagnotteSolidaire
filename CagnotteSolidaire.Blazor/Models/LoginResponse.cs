namespace CagnotteSolidaire.Blazor.Models;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}