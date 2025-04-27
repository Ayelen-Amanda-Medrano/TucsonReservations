using MediatR;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.WaitingList.Response;
using TucsonReservations.Application.WaitingList.Services.Interfaces;

namespace TucsonReservations.Application.WaitingList;

public record GetWaitingListQuery : IRequest<Result<GetWaitingListResponse>>
{
}
public class GetWaitingListHandler : IRequestHandler<GetWaitingListQuery, Result<GetWaitingListResponse>>
{
    private readonly IWaitingListService _waitingListService;

    public GetWaitingListHandler(IWaitingListService waitingListService)
    {
        _waitingListService = waitingListService;
    }

    public async Task<Result<GetWaitingListResponse>> Handle(GetWaitingListQuery request, CancellationToken cancellationToken)
        => _waitingListService.GetAll();
}