using System;
using System.Collections.Generic;

namespace FastBus.Domain.Entities
{
    public class DriverRequiringApproval
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public DateTime? DateBorn { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
