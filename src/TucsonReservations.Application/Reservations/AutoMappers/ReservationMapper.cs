using AutoMapper;
using TucsonReservations.Application.Reservations.Dtos;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.Reservations.AutoMappers;

public class ReservationMapper : Profile
{
    public ReservationMapper()
    {
        CreateMap<Reservation, ReservationDto>()
           .ForMember(dest => dest.MemberNumber, opt => opt.MapFrom(src => src.Client.MemberNumber))
           .ForMember(dest => dest.ReservationDate, opt => opt.MapFrom(src => src.ReservationDate));
    }
}
