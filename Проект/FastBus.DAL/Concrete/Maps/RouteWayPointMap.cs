using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class RouteWayPointMap : EntityTypeConfiguration<RouteWayPoint>
    {
        public RouteWayPointMap()
        {
            HasKey(c => new {c.RouteId, c.WayPointId});

            Property(c => c.Sequence).IsRequired();
        }
    }
}
