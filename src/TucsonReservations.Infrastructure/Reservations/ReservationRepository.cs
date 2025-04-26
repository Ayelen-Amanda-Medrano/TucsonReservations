using TucsonReservations.Application.Reservations.Repositories.Interfaces;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Infrastructure.Reservations;

public class ReservationRepository : IReservationRepository
{
    private readonly List<Reservation> _reservations = new List<Reservation>();

    public void Add(Reservation reservation)
    {
        _reservations.Add(reservation);
    }
}
