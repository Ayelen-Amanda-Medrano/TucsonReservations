using TucsonReservations.Domain.Entities;
using TucsonReservations.Domain.Enums;

namespace TucsonReservations.Domain.Constants;

public static class ClientsConstants
{
    public static readonly List<Client> Clients = new()
        {
           new Client { Id = 100, MemberNumber = 1, Name = "Marco Rodriguez", Category = ClientCategory.Classic },
           new Client { Id = 101, MemberNumber = 2, Name = "Isabel Ramirez", Category = ClientCategory.Gold },
           new Client { Id = 102, MemberNumber = 3, Name = "Juan Sanchez", Category = ClientCategory.Platinum },
           new Client { Id = 103, MemberNumber = 4, Name = "Micaela Gutierrez", Category = ClientCategory.Diamond }
        };
}
