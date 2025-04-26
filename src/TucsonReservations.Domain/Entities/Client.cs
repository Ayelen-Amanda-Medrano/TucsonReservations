using TucsonReservations.Domain.Enums;

namespace TucsonReservations.Domain.Entities;

public class Client
{
    public int Id { get; set; }
    public int MemberNumber { get; set; }
    public required string Name { get; set; }
    public ClientCategory Category { get; set; }

    public bool CanReserve(DateTime reservationDate)
    {
        var now = DateTime.UtcNow.Date;
        var daysUntilReservation = (reservationDate.Date - now).TotalDays;

        switch (Category)
        {
            case ClientCategory.Classic:
                return daysUntilReservation <= 2;
            case ClientCategory.Gold:
                return daysUntilReservation <= 3;
            case ClientCategory.Platinum:
                return daysUntilReservation <= 4;
            case ClientCategory.Diamond:
                return true;
            default:
                return false;
        }
    }
}
