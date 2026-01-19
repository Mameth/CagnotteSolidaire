using MediatR;
using Microsoft.AspNetCore.Mvc;
using CagnotteSolidaire.Domain.Commands.Utilisateurs;

namespace CagnotteSolidaire.API.Controllers;

[ApiController]
[Route("api/utilisateurs")]
public class UtilisateursController : ControllerBase
{
    private readonly IMediator _mediator;

    public UtilisateursController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // =========================
    // INSCRIPTION PARTICIPANT
    // =========================
    [HttpPost("participants")]
    public async Task<IActionResult> InscrireParticipant(
        [FromBody] InscrireParticipantCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    // =========================
    // INSCRIPTION GESTIONNAIRE
    // =========================
    [HttpPost("gestionnaires")]
    public async Task<IActionResult> InscrireGestionnaire(
        [FromBody] InscrireGestionnaireCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }
}
