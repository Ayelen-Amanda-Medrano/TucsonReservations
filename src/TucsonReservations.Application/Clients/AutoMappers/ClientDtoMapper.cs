using AutoMapper;
using TucsonReservations.Application.Clients.Dtos;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.Clients.AutoMappers;

public class ClientDtoMapper : Profile
{
    public ClientDtoMapper()
    {
        CreateMap<Client, ClientDto>();
    }
}
