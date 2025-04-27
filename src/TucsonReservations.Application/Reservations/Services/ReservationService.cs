using AutoMapper;
using System.Net;
using TucsonReservations.Application.Clients.Repositories.Interfaces;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.Reservations.Dtos;
using TucsonReservations.Application.Reservations.Repositories.Interfaces;
using TucsonReservations.Application.Reservations.Response;
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
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IClientRepository clientRepository, IWaitingListRepository waitingListRepository, ITableRepository tableRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _clientRepository = clientRepository;
        _waitingListRepository = waitingListRepository;
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public Result<CreateReservationResponse> Create(CreateReservationCommand request)
    {
        var reservationDateOnly = DateOnly.FromDateTime(request.ReservationDate);
        var client = _clientRepository.GetClientByMemberNumber(request.MemberNumber);
        if(client is null)
            return Result<CreateReservationResponse>.Fail($"Client with member number {request.MemberNumber} not found.", HttpStatusCode.NotFound);

        var canReserve = client!.CanReserve(request.ReservationDate);
        if (!canReserve)
            return Result<CreateReservationResponse>
                .Fail($"The category of client number {request.MemberNumber} does not allow creating a reservation for the date {reservationDateOnly}.", HttpStatusCode.Forbidden);

        var table = _tableRepository.GetFirstAvailable(reservationDateOnly);

        if (table is null)
        {
            _waitingListRepository.Add(new() { Client = client, RequestedDate = reservationDateOnly } );
            return Result<CreateReservationResponse>.Ok(null, HttpStatusCode.OK, "No tables available. Added to waiting list.");
        }

        var reservation = Reservation.CreateInstance(client, table, request.ReservationDate);
        _reservationRepository.Add(reservation);

        _tableRepository.ReserveTable(reservationDateOnly, table.TableNumber);

        return Result<CreateReservationResponse>.Ok(new CreateReservationResponse() { TableNumber = table.TableNumber }, HttpStatusCode.Created);
    }

    public Result<GetReservationsResponse> GetAll()
    {
        var reservations = _reservationRepository.GetAll();

        var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);

        return Result<GetReservationsResponse>.Ok(new GetReservationsResponse() { Reservations = reservationsDto}, HttpStatusCode.OK );
    }

    public Result<object> Delete(DeleteReservationCommand request)
    {
        var reservationDateOnly = DateOnly.FromDateTime(request.ReservationDate);

        var reservation = _reservationRepository.GetReservation(request.ReservationDate, request.TableNumber);
        if(reservation is null)
            return Result<object>.Fail($"Reservation for table number {request.TableNumber} not found on {reservationDateOnly}.", HttpStatusCode.NotFound);

        _reservationRepository.Remove(reservation);
        _tableRepository.FreeTable(reservationDateOnly, reservation.Table.TableNumber);


        var waitingListItem = _waitingListRepository.GetNextByPriority();
        if(waitingListItem is not null)
        {
            var table = _tableRepository.GetFirstAvailable(reservationDateOnly);
            if (table is not null && waitingListItem.RequestedDate == reservationDateOnly)
            {
                var newReservation = Reservation.CreateInstance(waitingListItem.Client, table, request.ReservationDate);

                _reservationRepository.Add(newReservation);
                _waitingListRepository.Remove(waitingListItem);
                _tableRepository.ReserveTable(reservationDateOnly, table!.TableNumber);
            }
        }

        return Result<object>.Ok(null, HttpStatusCode.NoContent);
    }
}
