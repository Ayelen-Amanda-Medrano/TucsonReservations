using TucsonReservations.Application.Table.Repositories.Interfaces;
using TucsonReservations.Domain.Constants;

namespace TucsonReservations.Infrastructure.Table;

public class TableRepository : ITableRepository
{
    private readonly List<Domain.Entities.Table> _tables;

    public TableRepository()
    {
        _tables = TablesConstants.Tables.Select(t => new Domain.Entities.Table
        {
            TableNumber = t.TableNumber,
            Capacity = t.Capacity,
            IsAvailable = t.IsAvailable
        }).ToList();
    }

    public Domain.Entities.Table? GetFirstAvailability()
    {
        return _tables
            .Where(t => t.IsAvailable)
            .OrderBy(t => t.IsAvailable)
            .FirstOrDefault();
    }

    public void SetTableAvailability(int tableNumber, bool isAvailable)
    {
        var table = _tables.FirstOrDefault(t => t.TableNumber == tableNumber);
        if (table != null)
        {
            table.IsAvailable = isAvailable;
        }
    }
}
