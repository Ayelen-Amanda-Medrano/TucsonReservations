using AutoMapper;
using TucsonReservations.Application.Clients.Dtos;
using TucsonReservations.Domain.Entities;

namespace TucsonReservations.Application.Clients.AutoMappers;

public class ClientMapper : Profile
{
    public ClientMapper()
    {
        CreateMap<Client, ClientDto>();
    }
}
