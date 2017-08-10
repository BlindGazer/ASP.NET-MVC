using System.Data.Entity;
using FastBus.Domain.Entities;
using FastBus.Domain.Entities.Identity;
using FastBus.Persistence.Maps;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FastBus.Persistence
{
    public class FastBusDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public FastBusDbContext() : base(nameof(FastBusDbContext))
        {
        }

        public static FastBusDbContext Create()
        {
            return new FastBusDbContext();
        }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Dispatcher> Dispatchers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<DriverRequiringApproval> DriversRequiringApproval { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CustomRoute> CustomRoutes { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<ScheduleItem> Schedule { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<WayPoint> WayPoints { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<HistoryLog> HistoryLog { get; set; }
        public DbSet<PropertieLog> PropertiesLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder bulder)
        {
            base.OnModelCreating(bulder);

            bulder.Configurations.Add(new UserMap());
            bulder.Configurations.Add(new RoleMap());
            bulder.Configurations.Add(new BuyerMap());
            bulder.Configurations.Add(new DispatcherMap());

            bulder.Configurations.Add(new CompanyMap());
            bulder.Configurations.Add(new CarMap());
            bulder.Configurations.Add(new DriverRequiringApprovalMap());
            bulder.Configurations.Add(new WayPointMap());
            bulder.Configurations.Add(new RouteMap());
            bulder.Configurations.Add(new ScheduleItemMap());
            bulder.Configurations.Add(new RouteWayPointMap());
            bulder.Configurations.Add(new CustomRouteMap());
            bulder.Configurations.Add(new TicketMap());
            bulder.Configurations.Add(new ReviewMap());
            bulder.Configurations.Add(new HistoryLogMap());
            bulder.Configurations.Add(new PropertieLogMap());

            bulder.Entity<UserRole>().ToTable("UserRoles");
            bulder.Entity<UserLogin>().ToTable("UserLogins");
            bulder.Entity<UserClaim>().ToTable("UserClaims");
        }
    }
}
