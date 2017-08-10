using System.Collections.Generic;
using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class ScheduleItem : BaseSchedule<long>
    {
        public int RouteId { get; set; }
        public byte Seats { get; set; }
        public int Number { get; set; }

        public virtual Dispatcher Dispatcher { get; set; }
        public virtual Car Car { get; set; }
        public virtual Route Route { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
