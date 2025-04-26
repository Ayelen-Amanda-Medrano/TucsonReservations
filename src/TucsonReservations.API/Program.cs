using TucsonReservations.Application.Clients.AutoMappers;
using TucsonReservations.Application.Clients.Repositories.Interfaces;
using TucsonReservations.Application.Clients.Services;
using TucsonReservations.Application.Clients.Services.Interfaces;
using TucsonReservations.Application.Reservations;
using TucsonReservations.Application.Reservations.Repositories.Interfaces;
using TucsonReservations.Application.Reservations.Services;
using TucsonReservations.Application.Reservations.Services.Interfaces;
using TucsonReservations.Application.Table.Repositories.Interfaces;
using TucsonReservations.Application.WaitingList.Repositories.Interfaces;
using TucsonReservations.Infrastructure.Clients;
using TucsonReservations.Infrastructure.Reservations;
using TucsonReservations.Infrastructure.Table;
using TucsonReservations.Infrastructure.WaitingList;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<ITableRepository, TableRepository>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
builder.Services.AddSingleton<IWaitingListRepository, WaitingListRepositoryMock>();

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateReservationHandler).Assembly)
);

builder.Services.AddAutoMapper(typeof(ClientDtoMapper).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
