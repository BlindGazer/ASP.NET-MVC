using System.Collections.Generic;
using FastBus.DAL.Constracts;

namespace FastBus.DAL.Concrete.Entities
{
    public class WayPoint: BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<RouteWayPoint> Routes { get; set; }
    }
}
