using System.Net;
using TucsonReservations.Application.Clients.Repositories.Interfaces;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.Reservations.Repositories.Interfaces;
using TucsonReservations.Application.Reservations.Services.Interfaces;
using TucsonReservations.Application.Table.Repositories.Interfaces;
using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.Reservations.Services;

public class ReservationService : IReservationService
{
    public readonly IReservationRepository _reservationRepository;
    public readonly IClientRepository _clientRepository;
    public readonly IWaitingListRepository _waitingListRepository;
    public readonly ITableRepository _tableRepository;

    public ReservationService(IReservationRepository reservationRepository, IClientRepository clientRepository, IWaitingListRepository waitingListRepository, ITableRepository tableRepository)
    {
        _reservationRepository = reservationRepository;
        _clientRepository = clientRepository;
        _waitingListRepository = waitingListRepository;
        _tableRepository = tableRepository;
    }

    public Result<int> Create(CreateReservationCommand request)
    {
        var client = _clientRepository.GetClientByMemberNumber(request.MemberNumber);
        if(client is null)
            return Result<int>.Fail($"Client with member number {request.MemberNumber} not found.", HttpStatusCode.NotFound);

        var canReserve = client!.CanReserve(request.ReservationDate);
        if (!canReserve)
            return Result<int>
                .Fail($"The category of client number {request.MemberNumber} does not allow creating a reservation for the date {request.ReservationDate}.", HttpStatusCode.Forbidden);

        var table = _tableRepository.GetFirstAvailability();

        if (table is null)
        {
            _waitingListRepository.Add(client);
            return Result<int>.Ok(-1, "No tables available. Added to waiting list.");
        }

        var reservation = Reservation.CreateInstance(client, table, request.ReservationDate);
        _reservationRepository.Add(reservation);

        _tableRepository.SetTableAvailability(table.TableNumber, false);

        return Result<int>.Ok(table.TableNumber);
    }
}
