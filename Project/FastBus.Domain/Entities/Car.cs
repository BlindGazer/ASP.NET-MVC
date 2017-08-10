using System.Collections.Generic;
using FastBus.Domain.Enums;
using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class Car: BaseEntity<int>
    {
        public string Model { get; set; }
        public string GovermentNumber { get; set; }
        public string Color { get; set; }
        public byte Seats { get; set; }
        public int? GarageNumber { get; set; }
        public short? Year { get; set; }
        public StatusCar Status { get; set; }
        
        public virtual ICollection<ScheduleItem> Schedule { get; set; }
        public virtual ICollection<CustomRoute> CustomRoutes { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<DriverRequiringApproval> DriversRequiringApproval { get; set; }
    }
}