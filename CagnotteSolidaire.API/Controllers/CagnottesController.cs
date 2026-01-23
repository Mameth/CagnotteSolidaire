using MediatR;
using Microsoft.AspNetCore.Mvc;
using CagnotteSolidaire.Domain.Commands.Cagnottes;
using CagnotteSolidaire.Domain.Queries.Cagnottes;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CagnotteSolidaire.Domain.Repositories; 

namespace CagnotteSolidaire.API.Controllers;

[ApiController]
[Route("api/cagnottes")]
[Authorize]
public class CagnottesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICagnotteRepository _cagnotteRepo; 

    public CagnottesController(IMediator mediator, ICagnotteRepository cagnotteRepo)
    {
        _mediator = mediator;
        _cagnotteRepo = cagnotteRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetMesCagnottes()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        var userId = Guid.Parse(userIdString);

        var cagnottes = await _cagnotteRepo.GetByGestionnaireId(userId); 

        var dtos = cagnottes.Select(c => new 
        {
            Id = c.Id,
            Titre = c.Nom,
            Description = c.Description,
            Objectif = c.Objectif.Value,
            MontantActuel = c.MontantActuel.Value,
            Statut = c.Statut.ToString()
        });

        return Ok(dtos);
    }

    [HttpPost]
    [Authorize(Roles = "Gestionnaire")]
    public async Task<IActionResult> Creer([FromBody] CreationCagnotteRequest request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdString!);

        var command = new CreerCagnotteCommand(
            request.Nom,
            request.Description,
            request.Objectif,
            userId 
        );

        var cagnotteId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = cagnotteId }, cagnotteId);
    }

    [HttpPost("{id}/participer")]
    [Authorize(Roles = "Participant")]
    public async Task<IActionResult> Participer(Guid id, [FromBody] ParticipationRequest request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdString!);

        var command = new ParticiperCagnotteCommand(
            id,
            request.Montant,
            userId 
        );

        await _mediator.Send(command);
        return Ok(new { message = "Participation enregistrée !" });
    }

    [HttpPost("{id}/cloturer")]
    [Authorize(Roles = "Gestionnaire")] 
    public async Task<IActionResult> Cloturer(Guid id)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdString!);

        var command = new CloturerCagnotteCommand(id, userId);
        
        await _mediator.Send(command);
        return Ok(new { message = "Clôturée." });
    }

    [HttpGet("{id}")]
    [AllowAnonymous] 
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetCagnotteByIdQuery(id);
        var cagnotte = await _mediator.Send(query);

        if (cagnotte == null) return NotFound();

        return Ok(new {
            Id = cagnotte.Id,
            Titre = cagnotte.Nom,
            Description = cagnotte.Description,
            Objectif = cagnotte.Objectif.Value,
            MontantActuel = cagnotte.MontantActuel.Value,
            Statut = cagnotte.Statut.ToString()
        });
    }
}

public class CreationCagnotteRequest
{
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Objectif { get; set; }
}

public class ParticipationRequest
{
    public decimal Montant { get; set; }
}