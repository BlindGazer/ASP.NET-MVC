using System.Collections.Generic;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Constracts;
using FastBus.DAL.Enums;

namespace FastBus.DAL.Concrete.Entities
{
    public class Car: BaseEntity<int>
    {
        public string Model { get; set; }
        public string GovermentNumber { get; set; }
        public string Color { get; set; }
        public int Seats { get; set; }
        public short? Year { get; set; }
        public StatusCar Status { get; set; }
        
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<CustomRoute> CustomRoutes { get; set; }
        public virtual ICollection<User> Drivers { get; set; }
        public virtual ICollection<DriverRequiringApproval> DriversRequiringApproval { get; set; }
    }
}
