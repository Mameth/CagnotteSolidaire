using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using CagnotteSolidaire.Blazor.Models;

namespace CagnotteSolidaire.Blazor.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task<string?> Login(LoginModel loginModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

        if (!response.IsSuccessStatusCode)
        {
            return "Email ou mot de passe incorrect.";
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        await _localStorage.SetItemAsync("authToken", result!.Token);

        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);

        return null;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}
