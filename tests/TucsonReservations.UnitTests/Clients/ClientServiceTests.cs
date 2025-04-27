using AutoMapper;
using FluentAssertions;
using NSubstitute;
using TucsonReservations.Application.Clients.Dtos;
using TucsonReservations.Application.Clients.Repositories.Interfaces;
using TucsonReservations.Application.Clients.Services;
using TucsonReservations.Domain.Enums;
using Xunit;

namespace TucsonReservations.UnitTests.Clients;

public class ClientServiceTests
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly ClientService _service;

    public ClientServiceTests()
    {
        _clientRepository = Substitute.For<IClientRepository>();
        _mapper = Substitute.For<IMapper>();
        _service = new ClientService(_clientRepository, _mapper);
    }

    [Fact]
    public void GetByMemberNumber_ClientExists_ReturnsMappedDto()
    {
        // Arrange
        var memberNumber = 123;
        var domainClient = new Domain.Entities.Client
        {
            MemberNumber = memberNumber,
            Name = "Alice",
            Category = Domain.Enums.ClientCategory.Gold
        };
        var expectedDto = new ClientDto
        {
            MemberNumber = memberNumber,
            Name = "Alice",
            Category = ClientCategory.Gold
        };

        _clientRepository
            .GetClientByMemberNumber(memberNumber)
            .Returns(domainClient);

        _mapper
            .Map<ClientDto?>(domainClient)
            .Returns(expectedDto);

        // Act
        var result = _service.GetByMemberNumber(memberNumber);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(expectedDto);

        _clientRepository.Received(1).GetClientByMemberNumber(memberNumber);
        _mapper.Received(1).Map<ClientDto?>(domainClient);
    }
}
