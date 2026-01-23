using CagnotteSolidaire.Blazor.Models;

namespace CagnotteSolidaire.Blazor.Services;

public interface IAuthService
{
    Task<string?> Login(LoginModel loginModel);
    Task Logout();
}