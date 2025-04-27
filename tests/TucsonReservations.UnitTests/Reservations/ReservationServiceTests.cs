using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Net;
using TucsonReservations.Application.Clients.Repositories.Interfaces;
using TucsonReservations.Application.Reservations;
using TucsonReservations.Application.Reservations.Dtos;
using TucsonReservations.Application.Reservations.Repositories.Interfaces;
using TucsonReservations.Application.Reservations.Services;
using TucsonReservations.Application.Reservations.Services.Interfaces;
using TucsonReservations.Application.Table.Repositories.Interfaces;
using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Domain.Entities;
using TucsonReservations.Domain.Enums;
using Xunit;

namespace TucsonReservations.UnitTests.Reservations;

public class ReservationServiceTests
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IWaitingListRepository _waitingListRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;
    private readonly IReservationService _service;

    public ReservationServiceTests()
    {
        _reservationRepository = Substitute.For<IReservationRepository>();
        _clientRepository = Substitute.For<IClientRepository>();
        _waitingListRepository = Substitute.For<IWaitingListRepository>();
        _tableRepository = Substitute.For<ITableRepository>();
        _mapper = Substitute.For<IMapper>();
        _service = new ReservationService(_reservationRepository, _clientRepository, _waitingListRepository, _tableRepository, _mapper);
    }

    [Fact]
    public void Create_ClientNotFound_ReturnsNotFound()
    {
        // Arrange
        var command = new CreateReservationCommand{ MemberNumber = 42, ReservationDate = DateTime.UtcNow.AddDays(1) };
        _clientRepository.GetClientByMemberNumber(42).Returns((Client)null);

        // Act
        var result = _service.Create(command);

        // Assert
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().Contain("Client with member number 42 not found");
    }

    [Fact]
    public void Create_ClientCannotReserve_ReturnsForbidden()
    {
        // Arrange
        var date = DateTime.UtcNow.AddDays(10);
        var command = new CreateReservationCommand { MemberNumber = 1, ReservationDate = DateTime.UtcNow.AddDays(5) };
        var client = new Client { Name = "Roberta", MemberNumber = 1, Category = ClientCategory.Classic };
        _clientRepository.GetClientByMemberNumber(1).Returns(client);
        client.Category = ClientCategory.Classic;

        // Act
        var result = _service.Create(command);

        // Assert
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.Message.Should().Contain("does not allow creating a reservation");
    }

    [Fact]
    public void Create_NoTableAvailable_AddsToWaitingListAndReturnsOk()
    {
        // Arrange
        var date = DateTime.UtcNow.AddDays(1);
        var dateOnly = DateOnly.FromDateTime(date);
        var command = new CreateReservationCommand { MemberNumber = 1, ReservationDate = date };
        var client = new Client { MemberNumber = 1, Category = ClientCategory.Diamond };
        _clientRepository.GetClientByMemberNumber(1).Returns(client);
        _tableRepository.GetFirstAvailable(dateOnly).Returns((Table)null);

        // Act
        var result = _service.Create(command);

        // Assert
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Message.Should().Contain("Added to waiting list");
        _waitingListRepository.Received(1).Add(Arg.Is<WaitingListItem>(w =>
            w.Client == client && w.RequestedDate == dateOnly));
    }

    [Fact]
    public void Create_TableAvailable_CreatesReservationAndReturnsCreated()
    {
        // Arrange
        var dateOnly = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
        var date = DateTime.UtcNow.AddDays(1);
        var command = new CreateReservationCommand { MemberNumber = 1, ReservationDate = date };
        var client = new Client { MemberNumber = 1, Category = ClientCategory.Diamond };
        var table = new Table { TableNumber = 5, Capacity = 4, IsAvailable = true };
        _clientRepository.GetClientByMemberNumber(Arg.Any<int>()).Returns(client);
        _tableRepository.GetFirstAvailable(Arg.Any<DateOnly>()).Returns(table);

        // Act
        var result = _service.Create(command);

        // Assert
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Data!.TableNumber.Should().Be(5);
        _reservationRepository.Received(1).Add(Arg.Any<Reservation>());
        _tableRepository.Received(1).ReserveTable(dateOnly, 5);
    }

    [Fact]
    public void GetAll_MapsAndReturnsReservations()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var reservations = new List<Reservation>
            {
                Reservation.CreateInstance(new Client { MemberNumber = 1, Category = ClientCategory.Classic },
                                           new Table { TableNumber = 2, Capacity = 2 },
                                           date)
            };
        var dtos = new List<ReservationDto> { new ReservationDto { MemberNumber = 1, ReservationDate =  date} };

        _reservationRepository.GetAll().Returns(reservations);
        _mapper.Map<List<ReservationDto>>(reservations).Returns(dtos);

        // Act
        var result = _service.GetAll();

        // Assert
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Data!.Reservations.Should().BeEquivalentTo(dtos);
    }

    [Fact]
    public void Delete_ReservationNotFound_ReturnsNotFound()
    {
        // Arrange
        var command = new DeleteReservationCommand { TableNumber = 42, ReservationDate = DateTime.UtcNow.AddDays(5) };
        _reservationRepository.GetReservation(Arg.Any<DateTime>(), Arg.Any<int>()).Returns((Reservation)null);

        // Act
        var result = _service.Delete(command);

        // Assert
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Message.Should().Contain("not found");
    }

    [Fact]
    public void Delete_ExistingReservation_FreesTable_And_ReassignsWaitingList()
    {
        // Arrange
        var dateTime = DateTime.UtcNow;
        var date = DateOnly.FromDateTime(dateTime);
        var command = new DeleteReservationCommand { TableNumber = 42, ReservationDate = dateTime };
        var client = new Client { MemberNumber = 1, Category = ClientCategory.Diamond };
        var table = new Table { TableNumber = 5, Capacity = 4 };
        var reservation = Reservation.CreateInstance(client, table, dateTime);
        var waitingEntry = new WaitingListItem { Client = client, RequestedDate = date };

        _reservationRepository.GetReservation(Arg.Any<DateTime>(), Arg.Any<int>()).Returns(reservation);
        _waitingListRepository.GetNextByPriority().Returns(waitingEntry);
        _tableRepository.GetFirstAvailable(Arg.Any<DateOnly>()).Returns(table);

        // Act
        var result = _service.Delete(command);

        // Assert
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        _reservationRepository.Received(1).Remove(reservation);
        _tableRepository.Received(1).FreeTable(date, 5);
        // Luego reasigna
        _reservationRepository.Received(1).Add(Arg.Any<Reservation>());
        _waitingListRepository.Received(1).Remove(waitingEntry);
        _tableRepository.Received(1).ReserveTable(date, 5);
    }
}
