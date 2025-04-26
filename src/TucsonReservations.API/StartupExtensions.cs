using System.Text.Json.Serialization;
using TucsonReservations.Application;
using TucsonReservations.Application.Reservations;
using TucsonReservations.Infrastructure;

namespace TucsonReservations.API;

public static class StartupExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateReservationCommand).Assembly));

        builder.Services
            .AddApplicationServices()
            .AddInfrastructureServices();
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
