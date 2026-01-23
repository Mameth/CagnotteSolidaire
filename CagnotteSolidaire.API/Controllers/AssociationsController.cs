using Microsoft.AspNetCore.Mvc;
using CagnotteSolidaire.Domain.Services;

namespace CagnotteSolidaire.API.Controllers;

[ApiController]
[Route("api/associations")]
public class AssociationsController : ControllerBase
{
    private readonly IJoAssociationService _joService;

    public AssociationsController(IJoAssociationService joService)
    {
        _joService = joService;
    }

    [HttpGet("recherche")]
    public async Task<IActionResult> Rechercher([FromQuery] string q)
    {
        Console.WriteLine($"[API] Recherche reçue pour : {q}"); // Log pour voir si ça arrive

        if (string.IsNullOrWhiteSpace(q))
            return BadRequest("Le terme de recherche est vide.");

        var resultats = await _joService.Rechercher(q, "68");

        return Ok(resultats);
    }
}