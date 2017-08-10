using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class CustomRouteMap: EntityTypeConfiguration<CustomRoute>
    {
        public CustomRouteMap()
        {
            HasMany(u => u.Drivers).WithMany(cr => cr.CustomRoutes).
                Map(m => m.ToTable("DriversCustomRoutes").MapLeftKey("DriverId").MapRightKey("CustomRouteId"));
        }
    }
}
