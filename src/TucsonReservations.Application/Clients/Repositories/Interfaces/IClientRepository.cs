using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.Clients.Repositories.Interfaces;

public interface IClientRepository
{
    Client? GetClientByMemberNumber(int memberNumber);
}
