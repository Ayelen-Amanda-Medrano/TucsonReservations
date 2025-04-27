using TucsonReservations.Application.Clients.Dtos;

namespace TucsonReservations.Application.WaitingList.Dtos;

public class WaitingListItemDto
{
    public required ClientDto client { get; set; }

    public DateOnly RequestedDate { get; set; }
}
