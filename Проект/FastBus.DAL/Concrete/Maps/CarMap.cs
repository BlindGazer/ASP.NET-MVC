using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class CarMap : EntityTypeConfiguration<Car>
    {
        public CarMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Model).IsRequired().HasMaxLength(50);
            Property(c =>c.GovermentNumber).IsRequired().HasMaxLength(20);
            Property(c => c.Color).IsRequired().HasMaxLength(20);
            Property(c => c.Seats).IsRequired();

            HasMany(c => c.Routes).WithRequired(r => r.Car).HasForeignKey(c => c.CarId).WillCascadeOnDelete(false);
            HasMany(c => c.CustomRoutes).WithRequired(cr => cr.Car).HasForeignKey(c => c.CarId).WillCascadeOnDelete(false);
        }
    }
}
