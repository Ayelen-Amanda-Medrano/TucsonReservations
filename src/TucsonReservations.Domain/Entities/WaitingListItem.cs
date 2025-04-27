namespace TucsonReservations.Domain.Entities;

public class WaitingListItem
{
    public required Client Client { get; set; }
    public DateOnly RequestedDate { get; set; }
}
