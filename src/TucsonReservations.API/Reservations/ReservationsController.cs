using MediatR;
using Microsoft.AspNetCore.Mvc;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.Reservations;
using TucsonReservations.Application.Reservations.Response;

namespace TucsonReservations.API.Reservations;

[ApiController]
[Route("/api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<CreateReservationResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
    {
        var result = await _mediator.Send(command);

        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<GetReservationsResponse>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetReservationsQuery());

        return StatusCode((int)result.StatusCode, result);
    }
}
