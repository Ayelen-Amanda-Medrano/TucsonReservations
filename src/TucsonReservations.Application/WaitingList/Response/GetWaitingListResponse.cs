using TucsonReservations.Application.WaitingList.Dtos;

namespace TucsonReservations.Application.WaitingList.Response;

public class GetWaitingListResponse
{
    public List<WaitingListItemDto> WaitingList { get; set; } = new();
}
