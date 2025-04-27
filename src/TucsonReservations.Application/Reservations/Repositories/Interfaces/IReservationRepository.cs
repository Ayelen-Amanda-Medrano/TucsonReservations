using TucsonReservations.Application.Reservations.Dtos;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.Reservations.Repositories.Interfaces;

public interface IReservationRepository
{
    void Add(Reservation reservation);

    IReadOnlyList<Reservation> GetAll();

    Reservation? GetReservation(DateTime reservationDate, int numberTable);
    void Remove(Reservation reservation);
}
