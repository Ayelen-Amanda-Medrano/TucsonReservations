namespace TucsonReservations.Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Client Client { get; set; }
    public Table Table { get; set; }
    public DateTime ReservationDate { get; set; }

    public static Reservation CreateInstance(Client client, Table table, DateTime ReservationDate)
    {
        var reservation = new Reservation
        {
            Client = client,
            Table = table,
            ReservationDate = ReservationDate
        };

        return reservation;
    }
}
