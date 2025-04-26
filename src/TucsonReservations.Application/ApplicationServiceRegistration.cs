using Microsoft.Extensions.DependencyInjection;
using TucsonReservations.Application.Clients.AutoMappers;
using TucsonReservations.Application.Clients.Services;
using TucsonReservations.Application.Clients.Services.Interfaces;
using TucsonReservations.Application.Reservations.Services;
using TucsonReservations.Application.Reservations.Services.Interfaces;

namespace TucsonReservations.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<IReservationService, ReservationService>();

        services.AddAutoMapper(typeof(ClientDtoMapper).Assembly);

        return services;
    }
}