using System.Collections.Generic;
using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class WayPoint: BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<RouteWayPoint> Routes { get; set; }
    }
}
