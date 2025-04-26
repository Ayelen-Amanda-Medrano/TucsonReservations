using TucsonReservations.Application.Common;

namespace TucsonReservations.Application.Reservations.Services.Interfaces;

public interface IReservationService
{
    Result<int> Create(CreateReservationCommand request);
}
