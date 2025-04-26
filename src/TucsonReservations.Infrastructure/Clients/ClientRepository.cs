using TucsonReservations.Application.Clients.Repositories.Interfaces;
using TucsonReservations.Domain.Constants;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Infrastructure.Clients;

public class ClientRepository : IClientRepository
{
    public Client? GetClientByMemberNumber(int memberNumber)
    {
        var client = ClientsConstants.Clients.FirstOrDefault(c => c.MemberNumber == memberNumber);
        return client;
    }
}
