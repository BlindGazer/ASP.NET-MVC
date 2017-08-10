using System.Collections.Generic;
using FastBus.Domain.Entities.Identity;

namespace FastBus.Domain.Entities
{
    public class Driver : User
    {
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<CustomRoute> CustomRoutes { get; set; }
        public virtual ICollection<ScheduleItem> Schedule { get; set; }
    }
}
