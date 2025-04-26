using TucsonReservations.Application.Reservations.Dtos;

namespace TucsonReservations.Application.Reservations.Response;

public class GetReservationsResponse
{
    public List<ReservationDto> Reservations { get; set; } = new();
}
