using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class RouteWayPointMap : EntityTypeConfiguration<RouteWayPoint>
    {
        public RouteWayPointMap()
        {
            HasKey(c => new {c.RouteId, c.WayPointId});
        }
    }
}
