using System.Collections.Generic;
using FastBus.Domain.Enums;
using FastBus.Domain.Objects;

namespace FastBus.Services.Models.Car
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string GovermentNumber { get; set; }
        public string Color { get; set; }
        public byte Seats { get; set; }
        public int? GarageNumber { get; set; }
        public short? Year { get; set; }
        public StatusCar Status { get; set; }
    }

    public class CarModelWithDrivers : CarModel
    {
        public IEnumerable<ListItem> Drivers { get; set; }
    }
}
