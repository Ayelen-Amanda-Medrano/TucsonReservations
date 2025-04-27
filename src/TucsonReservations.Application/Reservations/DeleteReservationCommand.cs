using MediatR;
using System.ComponentModel.DataAnnotations;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.Reservations.Services.Interfaces;

namespace TucsonReservations.Application.Reservations;

public record DeleteReservationCommand : IRequest<Result<object>>
{
    [Required]
    public int TableNumber { get; set; }

    [Required]
    public DateTime ReservationDate { get; set; }
}

public class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, Result<object>>
{
    private readonly IReservationService _reservationService;

    public DeleteReservationHandler(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public async Task<Result<object>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        => _reservationService.Delete(request);
}