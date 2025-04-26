namespace TucsonReservations.Domain.Entities;

public class Table
{
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
}
