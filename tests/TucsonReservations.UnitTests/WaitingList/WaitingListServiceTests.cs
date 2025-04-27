using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Net;
using TucsonReservations.Application.Clients.Dtos;
using TucsonReservations.Application.WaitingList.Dtos;
using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Application.WaitingList.Services;
using TucsonReservations.Domain.Entities;
using TucsonReservations.Domain.Enums;
using Xunit;

namespace TucsonReservations.UnitTests.WaitingList
{
    public class WaitingListServiceTests
    {
        private readonly IWaitingListRepository _waitingListRepository;
        private readonly IMapper _mapper;
        private readonly WaitingListService _service;

        public WaitingListServiceTests()
        {
            _waitingListRepository = Substitute.For<IWaitingListRepository>();
            _mapper = Substitute.For<IMapper>();
            _service = new WaitingListService(_waitingListRepository, _mapper);
        }

        [Fact]
        public void GetAll_ShouldReturnMappedWaitingList_WhenRepositoryReturnsData()
        {
            // Arrange
            var waitingListFromRepo = new List<WaitingListItem>();
            var mappedList = new List<WaitingListItemDto>
            {
                new WaitingListItemDto { Client = new ClientDto { Id = 100, MemberNumber = 1, Name = "Marco Rodriguez", Category = ClientCategory.Classic }, RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow) }
            };

            _waitingListRepository.GetAll().Returns(waitingListFromRepo);
            _mapper.Map<List<WaitingListItemDto>>(waitingListFromRepo).Returns(mappedList);

            // Act
            var result = _service.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.WaitingList.Should().HaveCount(1);
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            _waitingListRepository.Received(1).GetAll();
            _mapper.Received(1).Map<List<WaitingListItemDto>>(waitingListFromRepo);
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyList_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            var emptyList = new List<WaitingListItem>();
            var mappedEmptyList = new List<WaitingListItemDto>();

            _waitingListRepository.GetAll().Returns(emptyList);
            _mapper.Map<List<WaitingListItemDto>>(emptyList).Returns(mappedEmptyList);

            // Act
            var result = _service.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.WaitingList.Should().BeEmpty();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
