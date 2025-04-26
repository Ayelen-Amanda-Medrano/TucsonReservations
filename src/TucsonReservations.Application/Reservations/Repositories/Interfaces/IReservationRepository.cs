using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.Reservations.Repositories.Interfaces;

public interface IReservationRepository
{
    void Add(Reservation reservation);
}
