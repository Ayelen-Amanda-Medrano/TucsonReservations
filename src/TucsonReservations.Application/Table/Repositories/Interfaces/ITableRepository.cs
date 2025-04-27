namespace TucsonReservations.Application.Table.Repositories.Interfaces;

public interface ITableRepository
{
    Domain.Entities.Table? GetFirstAvailable(DateOnly date);

    bool ReserveTable(DateOnly date, int tableNumber);

    public bool FreeTable(DateOnly date, int tableNumber);
}
