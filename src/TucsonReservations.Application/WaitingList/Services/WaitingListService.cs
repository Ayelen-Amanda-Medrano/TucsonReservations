using AutoMapper;
using System.Net;
using TucsonReservations.Application.Common;
using TucsonReservations.Application.WaitingList.Dtos;
using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Application.WaitingList.Response;
using TucsonReservations.Application.WaitingList.Services.Interfaces;

namespace TucsonReservations.Application.WaitingList.Services;

public class WaitingListService : IWaitingListService
{
    private readonly IWaitingListRepository _waitingListRepository;
    private readonly IMapper _mapper;

    public WaitingListService(IWaitingListRepository waitingListRepository, IMapper mapper)
    {
        _waitingListRepository = waitingListRepository;
        _mapper = mapper;
    }
    public Result<GetWaitingListResponse> GetAll()
    {
        var waitingList = _waitingListRepository.GetAll();

        var waitingListDto = _mapper.Map<List<WaitingListItemDto>>(waitingList);

        return Result<GetWaitingListResponse>.Ok(new GetWaitingListResponse() { WaitingList = waitingListDto }, HttpStatusCode.OK);
    }
}
