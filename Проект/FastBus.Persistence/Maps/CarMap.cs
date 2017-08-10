using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class CarMap : EntityTypeConfiguration<Car>
    {
        public CarMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Model).IsRequired().HasMaxLength(50);
            Property(c =>c.GovermentNumber).IsRequired().HasMaxLength(20);
            Property(c => c.Color).IsRequired().HasMaxLength(20);
            Property(c => c.Seats).IsRequired();

            HasMany(c => c.Schedule).WithRequired(r => r.Car).HasForeignKey(c => c.CarId).WillCascadeOnDelete(false);
            HasMany(c => c.CustomRoutes).WithRequired(cr => cr.Car).HasForeignKey(c => c.CarId).WillCascadeOnDelete(false);

            HasMany(u => u.Drivers).WithMany(c => c.Cars).Map(m => m.ToTable("DriversCars").MapLeftKey("DriverId").MapRightKey("CarId"));
        }
    }
}
