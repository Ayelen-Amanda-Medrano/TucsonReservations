using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.WaitingList.Repositories.Interfaces;

public interface IWaitingListRepository
{
    void Add(Client client);
    void Remove(Client client);
    Client? GetNextByPriority();
    IReadOnlyList<Client> GetAll();
}
