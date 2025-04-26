using TucsonReservations.Application.Common;
using TucsonReservations.Application.Reservations.Response;

namespace TucsonReservations.Application.Reservations.Services.Interfaces;

public interface IReservationService
{
    Result<CreateReservationResponse> Create(CreateReservationCommand request);

    Result<GetReservationsResponse> GetAll();
}
