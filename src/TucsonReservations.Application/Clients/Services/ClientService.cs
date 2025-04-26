using AutoMapper;
using TucsonReservations.Application.Clients.Dtos;
using TucsonReservations.Application.Clients.Repositories.Interfaces;
using TucsonReservations.Application.Clients.Services.Interfaces;

namespace TucsonReservations.Application.Clients.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public ClientDto? GetByMemberNumber(int memberNumber)
    {
        var client = _clientRepository.GetClientByMemberNumber(memberNumber);

        return _mapper.Map<ClientDto?>(client);
    }
}
