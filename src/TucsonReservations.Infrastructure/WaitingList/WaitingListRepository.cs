using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Infrastructure.WaitingList;

public class WaitingListRepository : IWaitingListRepository
{
    private readonly List<WaitingListItem> _waitingList = new();

    public void Add(WaitingListItem item) => _waitingList.Add(item);

    public IReadOnlyList<WaitingListItem> GetAll()
        => _waitingList;

    public WaitingListItem? GetNextByPriority()
    {
        return _waitingList
            .OrderByDescending(c => c.Client.Category)
            .FirstOrDefault();
    }

    public void Remove(WaitingListItem item)
    {
        _waitingList.Remove(item);
    }
}
