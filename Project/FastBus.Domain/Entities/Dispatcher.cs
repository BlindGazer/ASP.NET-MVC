using System.Collections.Generic;
using FastBus.Domain.Entities.Identity;

namespace FastBus.Domain.Entities
{
    public class Dispatcher : User
    {
        public virtual ICollection<ScheduleItem> Schedule { get; set; }
        public virtual ICollection<CustomRoute> CustomRoutes { get; set; }
    }
}
