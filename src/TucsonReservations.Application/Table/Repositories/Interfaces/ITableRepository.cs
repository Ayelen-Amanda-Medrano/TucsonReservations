namespace TucsonReservations.Application.Table.Repositories.Interfaces;

public interface ITableRepository
{
    Domain.Entities.Table? GetFirstAvailability();

    void SetTableAvailability(int tableNumber, bool isAvailable);
}
