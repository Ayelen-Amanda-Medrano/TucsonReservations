using MediatR;
using Microsoft.AspNetCore.Mvc;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.WaitingList;
using TucsonReservations.Application.WaitingList.Response;

namespace TucsonReservations.API.WaitingList;

[ApiController]
[Route("/api/waiting-list")]
public class WaitingListController : ControllerBase
{
    private readonly IMediator _mediator;

    public WaitingListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<GetWaitingListResponse>))]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetWaitingListQuery());

        return StatusCode((int)result.StatusCode, result);
    }
}
