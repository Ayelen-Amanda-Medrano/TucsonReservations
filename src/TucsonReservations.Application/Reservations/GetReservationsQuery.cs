using MediatR;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.Reservations.Response;
using TucsonReservations.Application.Reservations.Services.Interfaces;

namespace TucsonReservations.Application.Reservations;

public record GetReservationsQuery : IRequest<Result<GetReservationsResponse>>
{
}
public class GetReservationsHandler : IRequestHandler<GetReservationsQuery, Result<GetReservationsResponse>>
{
    private readonly IReservationService _reservationService;

    public GetReservationsHandler(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public async Task<Result<GetReservationsResponse>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        => _reservationService.GetAll();
}