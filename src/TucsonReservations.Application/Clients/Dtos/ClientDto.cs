using TucsonReservations.Domain.Enums;

namespace TucsonReservations.Application.Clients.Dtos;

public class ClientDto
{
    public int Id { get; set; }
    public int MemberNumber { get; set; }
    public required string Name { get; set; }
    public ClientCategory Category { get; set; }
}
