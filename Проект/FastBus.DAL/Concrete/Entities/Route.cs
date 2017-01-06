using System.Collections.Generic;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Constracts;

namespace FastBus.DAL.Concrete.Entities
{
    public class Route : BaseRoute<long>
    {
        public virtual User Creater { get; set; }
        public virtual Car Car { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<User> Drivers { get; set; }
        public virtual ICollection<RouteWayPoint> WayPoints { get; set; }

    }
}
