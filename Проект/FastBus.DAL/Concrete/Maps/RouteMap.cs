using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class RouteMap : EntityTypeConfiguration<Route>
    {
        public RouteMap()
        {
            HasKey(r => r.Id);
            Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(r => r.Departure).IsRequired().HasMaxLength(100);
            Property(r => r.Destination).IsRequired().HasMaxLength(100);

            HasRequired(r => r.Creater).WithMany(u => u.CreaterRoutes).HasForeignKey(r => r.CreaterId).WillCascadeOnDelete(false);
            HasRequired(r => r.Car).WithMany(c => c.Routes).HasForeignKey(r => r.CarId).WillCascadeOnDelete(false);

            HasMany(r => r.Tickets).WithRequired(t => t.Route).HasForeignKey(t => t.RouteId).WillCascadeOnDelete(false);
            HasMany(r => r.WayPoints).WithRequired(wp => wp.Route).HasForeignKey(t => t.RouteId).WillCascadeOnDelete(false);
        }
    }
}
