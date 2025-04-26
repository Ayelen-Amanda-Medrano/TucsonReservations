using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Infrastructure.WaitingList;

public class WaitingListRepositoryMock : IWaitingListRepository
{
    private readonly List<Client> _waitingList = new();
    public void Add(Client client) => _waitingList.Add(client);

    public IReadOnlyList<Client> GetAll()
    {
        throw new NotImplementedException();
    }

    public Client? GetNextByPriority()
    {
        throw new NotImplementedException();
    }

    public void Remove(Client client)
    {
        throw new NotImplementedException();
    }
}
