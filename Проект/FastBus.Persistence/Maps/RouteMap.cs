using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class RouteMap : EntityTypeConfiguration<Route>
    {
        public RouteMap()
        {
            HasKey(r => r.Id);

            Property(r => r.Departure).IsRequired().HasMaxLength(100);
            Property(r => r.Destination).IsRequired().HasMaxLength(100);

            HasMany(r => r.WayPoints).WithRequired(wp => wp.Route).HasForeignKey(t => t.RouteId).WillCascadeOnDelete(true);
            HasMany(r => r.Schedule).WithRequired(wp => wp.Route).HasForeignKey(t => t.RouteId).WillCascadeOnDelete(false);
        }
    }
}
