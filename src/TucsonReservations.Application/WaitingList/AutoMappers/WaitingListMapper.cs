using AutoMapper;
using TucsonReservations.Application.WaitingList.Dtos;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.WaitingList.AutoMappers;

public class WaitingListMapper : Profile
{
    public WaitingListMapper()
    {
        CreateMap<WaitingListItem, WaitingListItemDto>();
    }
}