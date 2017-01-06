using System;
using System.Collections.Generic;

namespace FastBus.DAL.Concrete.Entities
{
    public class DriverRequiringApproval
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime? DateBorn { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

    }
}
