using TucsonReservations.Application.Clients.Dtos;

namespace TucsonReservations.Application.WaitingList.Response;

public class GetWaitingListResponse
{
    public List<ClientDto> WaitingList { get; set; } = new();
}
