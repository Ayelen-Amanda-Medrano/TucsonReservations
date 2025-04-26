using Microsoft.Extensions.DependencyInjection;
using TucsonReservations.Application.Clients.Repositories.Interfaces;
using TucsonReservations.Application.Reservations.Repositories.Interfaces;
using TucsonReservations.Application.Table.Repositories.Interfaces;
using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Infrastructure.Clients;
using TucsonReservations.Infrastructure.Reservations;
using TucsonReservations.Infrastructure.Table;
using TucsonReservations.Infrastructure.WaitingList;

namespace TucsonReservations.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        => services.AddPersistenceServices();

    private static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IWaitingListRepository, WaitingListRepository>();

        return services;
    }
}
