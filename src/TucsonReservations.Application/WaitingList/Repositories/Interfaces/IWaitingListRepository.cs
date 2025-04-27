using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.WaitingList.Repositories.Interfaces;

public interface IWaitingListRepository
{
    void Add(WaitingListItem item);
    void Remove(WaitingListItem item);
    WaitingListItem? GetNextByPriority();
    IReadOnlyList<WaitingListItem> GetAll();
}
