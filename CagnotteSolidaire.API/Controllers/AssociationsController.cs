using MediatR;
using Microsoft.AspNetCore.Mvc;
using CagnotteSolidaire.Domain.Queries.Associations;

namespace CagnotteSolidaire.API.Controllers;

[ApiController]
[Route("api/associations")]
public class AssociationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssociationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("recherche")]
    public async Task<IActionResult> Rechercher([FromQuery] string q)
    {
        var result = await _mediator.Send(
            new RechercherAssociationsQuery(q));

        return Ok(result);
    }
}
