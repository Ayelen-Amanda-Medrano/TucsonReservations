using TucsonReservations.Application.Table.Repositories.Interfaces;
using TucsonReservations.Domain.Constants;

namespace TucsonReservations.Infrastructure.Table;

public class TableRepository : ITableRepository
{
    private readonly Dictionary<DateOnly, List<Domain.Entities.Table>> _tables = new Dictionary<DateOnly, List<Domain.Entities.Table>>();

    public bool ReserveTable(DateOnly date, int tableNumber)
    {
        var tables = GetTablesForDate(date);
        var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber && t.IsAvailable);
        if (table == null) return false;

        table.IsAvailable = false;
        return true;
    }

    public bool FreeTable(DateOnly date, int tableNumber)
    {
        var tables = GetTablesForDate(date);
        var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber && !t.IsAvailable);
        if (table == null) return false;

        table.IsAvailable = true;
        return true;
    }

    public Domain.Entities.Table? GetFirstAvailable(DateOnly date)
    {
        return GetTablesForDate(date)
               .FirstOrDefault(t => t.IsAvailable);
    }

    private List<Domain.Entities.Table> GetTablesForDate(DateOnly date)
    {
        if (!_tables.TryGetValue(date, out var tables))
        {
            tables = TablesConstants.Tables
                .Select(t => new Domain.Entities.Table
                {
                    TableNumber = t.TableNumber,
                    Capacity = t.Capacity,
                    IsAvailable = t.IsAvailable
                })
                .ToList();

            _tables[date] = tables;
        }

        return tables;
    }
}
