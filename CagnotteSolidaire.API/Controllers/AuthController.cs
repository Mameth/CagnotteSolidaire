using Microsoft.AspNetCore.Mvc;
using CagnotteSolidaire.Domain.Repositories;
using CagnotteSolidaire.Domain.Services;

namespace CagnotteSolidaire.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUtilisateurRepository _repo;
    private readonly ITokenService _tokenService;

    public AuthController(IUtilisateurRepository repo, ITokenService tokenService)
    {
        _repo = repo;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        
        var utilisateur = await _repo.GetByEmail(request.Email);
        
        if (utilisateur == null)
            return Unauthorized("Email inconnu.");

        
        var token = _tokenService.GenerateToken(utilisateur);

        return Ok(new { token = token });
    }
}
public record LoginRequest(string Email, string Password);