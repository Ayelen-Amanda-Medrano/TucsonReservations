using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Infrastructure.WaitingList;

public class WaitingListRepository : IWaitingListRepository
{
    private readonly List<Client> _waitingList = new();

    public void Add(Client client) => _waitingList.Add(client);

    public IReadOnlyList<Client> GetAll()
        => _waitingList;

    public Client? GetNextByPriority()
    {
        return _waitingList
            .OrderByDescending(c => c.Category)
            .FirstOrDefault();
    }

    public void Remove(Client client)
    {
        _waitingList.Remove(client);
    }
}
