using TucsonReservations.Domain.Entities;
using TucsonReservations.Domain.Enums;

namespace TucsonReservations.Domain.Constants;

public static class ClientsConstants
{
    public static readonly List<Client> Clients = new()
        {
           new Client { MemberNumber = 1, Name = "Marco Rodriguez", Category = ClientCategory.Classic },
           new Client { MemberNumber = 2, Name = "Isabel Ramirez", Category = ClientCategory.Gold },
           new Client { MemberNumber = 3, Name = "Juan Sanchez", Category = ClientCategory.Platinum },
           new Client { MemberNumber = 4, Name = "Micaela Gutierrez", Category = ClientCategory.Diamond }
        };
}
