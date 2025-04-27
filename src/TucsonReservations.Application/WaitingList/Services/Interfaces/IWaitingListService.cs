using TucsonReservations.Application.Common;
using TucsonReservations.Application.WaitingList.Response;

namespace TucsonReservations.Application.WaitingList.Services.Interfaces;

public interface IWaitingListService
{
    Result<GetWaitingListResponse> GetAll();
}
