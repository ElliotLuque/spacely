using System.CommandLine;

using CLI.Commands.Reservations;
using CLI.Commands.Spaces;
using CLI.Commands.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ReservationSystem.Reservations.Domain;
using ReservationSystem.Reservations.Infrastructure.Persistence;
using ReservationSystem.Shared.Infrastructure;
using ReservationSystem.Spaces.Domain;
using ReservationSystem.Spaces.Infrastructure.Persistence;
using ReservationSystem.Users.Domain;
using ReservationSystem.Users.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
  .ConfigureAppConfiguration((config) =>
  {
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
  })
  .ConfigureServices((context, services) =>
  {
    services.AddDbContext<ReservationSystemDbContext>(options =>
    {
      options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection"));
    });

    services.AddScoped<IReservationRepository, PostgresReservationRepository>();
    services.AddScoped<IUserRepository, PostgresUserRepository>();
    services.AddScoped<ISpaceRepository, PostgresSpaceRepository>();
  })
  .Build();

var root = new RootCommand("Spacely-CLI");
var scope = host.Services.CreateScope();
var reservationRepo = scope.ServiceProvider.GetRequiredService<IReservationRepository>();
var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
var spaceRepo = scope.ServiceProvider.GetRequiredService<ISpaceRepository>();

var create = new Command("create", "Create entities");
create.AddCommand(new CreateReservationCliCommand(reservationRepo, userRepo, spaceRepo).Build());
create.AddCommand(new CreateSpaceCliCommand(spaceRepo).Build());
create.AddCommand(new CreateUserCliCommand(userRepo).Build());

var find = new Command("find", "Find entities");
find.AddCommand(new FindReservationByIdCliCommand(reservationRepo).Build());

root.AddCommand(create);
root.AddCommand(find);


return await root.InvokeAsync(args);
