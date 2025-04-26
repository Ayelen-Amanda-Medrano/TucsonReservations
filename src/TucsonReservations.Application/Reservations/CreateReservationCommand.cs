using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TucsonReservations.Application.Clients.Services.Interfaces;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.Reservations.Response;
using TucsonReservations.Application.Reservations.Services.Interfaces;

namespace TucsonReservations.Application.Reservations;

public record CreateReservationCommand : IRequest<Result<CreateReservationResponse>>
{
    [Required]
    public int MemberNumber { get; set; }

    [Required]
    public DateTime ReservationDate { get; set; }
}

public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, Result<CreateReservationResponse>>
{
    private readonly IReservationService _reservationService;
    private readonly IClientService _clientService;

    public CreateReservationHandler(IReservationService reservationService, IClientService clientService)
    {
        _reservationService = reservationService;
        _clientService = clientService;
    }

    public async Task<Result<CreateReservationResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var client = _clientService.GetByMemberNumber(request.MemberNumber);
        if (client is null)
            return Result<CreateReservationResponse>.Fail($"Client with member number {request.MemberNumber} not found.", HttpStatusCode.NotFound);

        var reservationResult = _reservationService.Create(request);

        return reservationResult;
    }
}