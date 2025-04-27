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

    public IReadOnlyList<Reservation> GetAll()
        => _reservations;

    public Reservation? GetReservation(DateTime reservationDate, int numberTable)
        => _reservations.FirstOrDefault(r =>
            DateOnly.FromDateTime(r.ReservationDate) == DateOnly.FromDateTime(reservationDate) &&
            r.Table.TableNumber == numberTable);

    public void Remove(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }
}
