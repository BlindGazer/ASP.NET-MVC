using System.Collections.Generic;
using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class Route : BaseEntity<int>
    {
        public string Departure { get; set; }
        public string Destination { get; set; }

        public virtual ICollection<RouteWayPoint> WayPoints { get; set; }
        public virtual ICollection<ScheduleItem> Schedule { get; set; }
    }
}
