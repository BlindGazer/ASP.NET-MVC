using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using FastBus.Domain.Entities;
using FastBus.Domain.Entities.Identity;
using FastBus.Domain.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FastBus.Persistence.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<FastBusDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FastBusDbContext context)
        {
            #region CreateRoles 
            var roleStore = new RoleStore<Role, int, UserRole>(context);
            var roleManager = new RoleManager<Role, int>(roleStore);
            roleManager.Create(new Role {Name = UserRoles.Admin, Description = UserRoles.AdminDescription });
            roleManager.Create(new Role { Name = UserRoles.Dispatcher, Description = UserRoles.DispatcherDescription});
            roleManager.Create(new Role { Name = UserRoles.Driver, Description = UserRoles.DriverDescription });
            roleManager.Create(new Role { Name = UserRoles.Buyer, Description = UserRoles.ClientDescription });
            context.SaveChanges();
            #endregion

            #region CreateUsers
            var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context);
            var userManager = new UserManager<User, int>(userStore);

            if (!context.Users.Any(n => n.UserName == UserRoles.Admin))
            {
                var user = new User
                {
                    UserName = UserRoles.Admin,
                    FirstName = UserRoles.AdminDescription,
                    LastName = UserRoles.AdminDescription,
                    RegistredDate = DateTime.Now,
                    Email = "admin@example.com"
                };
                userManager.Create(user, "Qq!123");
                userManager.AddToRole(user.Id, UserRoles.Admin);
            }

            if (!context.Users.Any(n => n.UserName == UserRoles.Dispatcher))
            {
                var user = new Dispatcher
                {
                    UserName = UserRoles.Dispatcher,
                    FirstName = "Павел",
                    LastName = "Макаренко",
                    Patronymic = "Иванович",
                    RegistredDate = DateTime.Now,
                    DateBorn = DateTime.Now.AddYears(-20),
                    Email = "dispatcher@example.com"
                };
                userManager.Create(user, "Qq!123");
                userManager.AddToRole(user.Id, UserRoles.Dispatcher);
            }
            
            context.SaveChanges();
            #endregion

            #region CreateData
            
            if (!context.Cars.Any())
            {
                context.Cars.AddRange(new[]
                {
                    new Car
                    {
                        GovermentNumber = "AA3338PP",
                        Year = 1999,
                        Model = "Audi",
                        Color = "White",
                        Seats = 14
                    },
                    new Car
                    {
                        GovermentNumber = "A999AA",
                        Year = 2000,
                        Model = "BWM",
                        Color = "Black",
                        Seats = 32
                    },
                    new Car
                    {
                        GovermentNumber = "08483AE",
                        Year = 2001,
                        Model = "Ford",
                        Color = "Blue",
                        Seats = 32
                    },
                    new Car
                    {
                        GovermentNumber = "019140B",
                        Year = 2000,
                        Model = "Volkswagen",
                        Color = "Blue",
                        Seats = 20
                    },
                    new Car
                    {
                        GovermentNumber = "E102CH",
                        Year = 2003,
                        Model = "Audi",
                        Color = "White",
                        Seats = 20
                    },
                    new Car
                    {
                        GovermentNumber = "C065MK",
                        Year = 1995,
                        Model = "Audi",
                        Color = "Black",
                        Seats = 14
                    }
                });
                context.SaveChanges();

                var cars = context.Cars.ToList();
                if (!context.Users.Any(n => n.UserName == UserRoles.Driver))
                {
                    var user = new Driver
                    {
                        UserName = UserRoles.Driver,
                        FirstName = "Мстислав",
                        LastName = "Данилов",
                        Patronymic = "Святославович",
                        RegistredDate = DateTime.Now,
                        DateBorn = DateTime.Now.AddYears(-18).AddMonths(3).AddDays(15),
                        Cars = new List<Car> { cars[0], cars[1]}
                    };
                    userManager.Create(user, "Qq!123");
                    userManager.AddToRole(user.Id, UserRoles.Driver);
                }

                if (!context.Users.Any(n => n.UserName == UserRoles.Driver + "2"))
                {
                    var user = new Driver
                    {
                        UserName = UserRoles.Driver + "2",
                        FirstName = "Елена",
                        LastName = "Попова",
                        Patronymic = "Лукьяновн",
                        RegistredDate = DateTime.Now,
                        DateBorn = DateTime.Now.AddYears(-19).AddMonths(3).AddDays(7),
                        Cars = new List<Car> { cars[2], cars[3], cars[4] }
                    };
                    userManager.Create(user, "Qq!123");
                    userManager.AddToRole(user.Id, UserRoles.Driver);
                }

                if (!context.Users.Any(n => n.UserName == UserRoles.Driver + "3"))
                {
                    var user = new Driver
                    {
                        UserName = UserRoles.Driver + "3",
                        FirstName = "Авдей",
                        LastName = "Тарасов",
                        Patronymic = "Авдеевич",
                        RegistredDate = DateTime.Now,
                        DateBorn = DateTime.Now.AddYears(-21).AddMonths(11).AddDays(2),
                        Cars = new List<Car> { cars[4]}
                    };
                    userManager.Create(user, "Qq!123");
                    userManager.AddToRole(user.Id, UserRoles.Driver);
                }

                context.SaveChanges();
            }

            if (!context.Routes.Any())
            {
                context.Routes.AddRange(new List<Route>
                {
                    new Route
                    {
                        Departure = "Пункт А",
                        Destination = "Пункт Б"
                    },
                    new Route
                    {
                        Departure = "Пункт А",
                        Destination = "Пункт С"
                    },
                    new Route
                    {
                        Departure = "Пункт А",
                        Destination = "Пункт Д"
                    },
                    new Route
                    {
                        Departure = "Пункт А",
                        Destination = "Пункт И"
                    },
                    new Route
                    {
                        Departure = "Пункт А",
                        Destination = "Пункт Ф"
                    },
                    new Route
                    {
                        Departure = "Пункт А",
                        Destination = "Пункт Т"
                    },
                    new Route
                    {
                        Departure = "Пункт А",
                        Destination = "Пункт К"
                    },
                    new Route
                    {
                        Departure = "Пункт Б",
                        Destination = "Пункт А"
                    },
                    new Route
                    {
                        Departure = "Пункт С",
                        Destination = "Пункт А"
                    },
                    new Route
                    {
                        Departure = "Пункт Д",
                        Destination = "Пункт А"
                    },
                    new Route
                    {
                        Departure = "Пункт И",
                        Destination = "Пункт А"
                    },
                    new Route
                    {
                        Departure = "Пункт Ф",
                        Destination = "Пункт А"
                    },
                    new Route
                    {
                        Departure = "Пункт Т",
                        Destination = "Пункт А"
                    },
                    new Route
                    {
                        Departure = "Пункт К",
                        Destination = "Пункт А"
                    },
                    new Route
                    {
                        Departure = "Пункт Ф",
                        Destination = "Пункт З"
                    },
                    new Route
                    {
                        Departure = "Пункт Ф",
                        Destination = "Пункт Т"
                    },
                    new Route
                    {
                        Departure = "Пункт Т",
                        Destination = "Пункт З"
                    },
                    new Route
                    {
                        Departure = "Пункт К",
                        Destination = "Пункт Ф"
                    },
                    new Route
                    {
                        Departure = "Пункт Ф",
                        Destination = "Пункт К"
                    }
                });

                context.SaveChanges();

                var routes = context.Routes.ToList();
                var dispatcher = context.Dispatchers.FirstOrDefault();
                var drivers = context.Drivers.ToList();
                var routeCars = context.Cars.ToList();
                var currentDepDate = DateTime.Now;
                var currentDesDate = DateTime.Now.AddHours(4);
                if (dispatcher != null && drivers.Count > 2 && routeCars.Count >= 4)
                {
                    context.Schedule.AddRange(new List<ScheduleItem>
                    {
                        new ScheduleItem
                        {
                            RouteId = routes[0].Id,
                            Number = 1000,
                            DepartureDate = currentDepDate,
                            DestinationDate = currentDesDate,
                            Seats = routeCars[0].Seats,
                            CarId = routeCars[0].Id,
                            DispatcherId = dispatcher.Id,
                            Cost = 100,
                            Drivers = new List<Driver> { drivers.First(x => x.Cars.Any(c => c.Id == routeCars[0].Id)) }
                        },
                        new ScheduleItem
                        {
                            RouteId = routes[0].Id,
                            Number = 1000,
                            DepartureDate = currentDepDate.AddDays(1),
                            DestinationDate = currentDesDate.AddDays(1),
                            Seats = routeCars[0].Seats,
                            CarId = routeCars[0].Id,
                            DispatcherId = dispatcher.Id,
                            Cost = 100,
                            Drivers = new List<Driver> { drivers.First(x => x.Cars.Any(c => c.Id == routeCars[0].Id)) }
                        },
                        new ScheduleItem
                        {
                            RouteId = routes[7].Id,
                            Number = 1001,
                            DepartureDate = currentDepDate.AddMinutes(30),
                            DestinationDate = currentDesDate.AddMinutes(30),
                            Seats = routeCars[0].Seats,
                            CarId = routeCars[0].Id,
                            DispatcherId = dispatcher.Id,
                            Cost = 100,
                            Drivers = new List<Driver> { drivers.First(x => x.Cars.Any(c => c.Id == routeCars[0].Id)) }
                        },
                        new ScheduleItem
                        {
                            RouteId = routes[7].Id,
                            Number = 1001,
                            DepartureDate = currentDepDate.AddDays(1),
                            DestinationDate = currentDesDate.AddDays(1),
                            Seats = routeCars[0].Seats,
                            CarId = routeCars[0].Id,
                            DispatcherId = dispatcher.Id,
                            Cost = 100,
                            Drivers = new List<Driver> { drivers.First(x => x.Cars.Any(c => c.Id == routeCars[0].Id)) }
                        },
                        new ScheduleItem
                        {
                            RouteId = routes[1].Id,
                            Number = 1002,
                            DepartureDate = currentDepDate.AddHours(2),
                            DestinationDate = currentDesDate.AddHours(3),
                            Seats = routeCars[1].Seats,
                            CarId = routeCars[1].Id,
                            DispatcherId = dispatcher.Id,
                            Cost = 90,
                            Drivers = new List<Driver> { drivers.First(x => x.Cars.Any(c => c.Id == routeCars[2].Id)) }
                        },
                        new ScheduleItem
                        {
                            RouteId = routes[8].Id,
                            Number = 1003,
                            DepartureDate = currentDepDate.AddHours(-3),
                            DestinationDate = currentDesDate.AddHours(-3),
                            Seats = routeCars[1].Seats,
                            CarId = routeCars[1].Id,
                            DispatcherId = dispatcher.Id,
                            Cost = 95,
                            Drivers = new List<Driver> { drivers.First(x => x.Cars.Any(c => c.Id == routeCars[2].Id)) }
                        },
                        new ScheduleItem
                        {
                            RouteId = routes[3].Id,
                            Number = 1005,
                            DepartureDate = currentDepDate,
                            DestinationDate = currentDesDate,
                            Seats = routeCars[2].Seats,
                            CarId = routeCars[2].Id,
                            DispatcherId = dispatcher.Id,
                            Cost = 100,
                            Drivers = new List<Driver> { drivers.First(x => x.Cars.Any(c => c.Id == routeCars[4].Id)) }
                        },
                        new ScheduleItem
                        {
                            RouteId = routes[4].Id,
                            Number = 1004,
                            DepartureDate = currentDepDate.AddDays(-1),
                            DestinationDate = currentDesDate.AddDays(-1),
                            Seats = routeCars[2].Seats,
                            CarId = routeCars[2].Id,
                            DispatcherId = dispatcher.Id,
                            Cost = 100,
                            Drivers = new List<Driver> { drivers.First(x => x.Cars.Any(c => c.Id == routeCars[4].Id)) }
                        }
                    });
                }

                context.SaveChanges();
            }

            #endregion
        }
    }
}
