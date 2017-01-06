using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities.Identity;

namespace FastBus.DAL.Concrete.Maps
{
    public class UserMap: EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");

            Property(u => u.RegistredDate).HasColumnType("date");
            Property(u => u.DateBorn).HasColumnType("date");
            Property(u => u.Name).IsRequired().HasMaxLength(50);
            Property(u => u.UserName).IsRequired().HasMaxLength(50);

            HasMany(u => u.Reviews).WithRequired(r => r.User).HasForeignKey(r => r.UserId);
            HasMany(u => u.Tickets).WithRequired(t => t.User).HasForeignKey(t => t.UserId);
            HasMany(u => u.Orders).WithRequired(cr => cr.Customer).HasForeignKey(cr => cr.CustomerId);
            HasMany(u => u.DriverCustomRoutes).WithMany(cr => cr.Drivers).Map(m => m.ToTable("DriversCustomRoutes").MapLeftKey("DriverId").MapRightKey("CustomRouteId"));
            HasMany(u => u.Cars).WithMany(c => c.Drivers).Map(m => m.ToTable("DriversCars").MapLeftKey("DriverId").MapRightKey("CarId"));
            HasMany(u => u.CreaterCustomRoutes).WithRequired(cr => cr.Creater).HasForeignKey(cr=>cr.CreaterId).WillCascadeOnDelete(false);
            HasMany(u => u.DriverRoutes).WithMany(r => r.Drivers).Map(m => m.ToTable("DriversRoutes").MapLeftKey("DriverId").MapRightKey("RouteId"));
            HasMany(u => u.CreaterRoutes).WithRequired(r => r.Creater).HasForeignKey(r => r.CreaterId).WillCascadeOnDelete(false);
        }
    }
}
