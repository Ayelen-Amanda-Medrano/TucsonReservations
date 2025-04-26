using TucsonReservations.Application.Clients.Dtos;

namespace TucsonReservations.Application.Clients.Services.Interfaces;

public interface IClientService
{
    ClientDto? GetByMemberNumber(int memberNumber);
}
