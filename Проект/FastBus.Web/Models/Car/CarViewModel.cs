using System.Collections.Generic;
using FastBus.DAL.Enums;

namespace FastBus.Web.Models.Car
{
    public class DriverCarViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CarViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string GovermentNumber { get; set; }
        public string Color { get; set; }
        public int Seats { get; set; }
        public short? Year { get; set; }
        public StatusCar Status { get; set; }
        public IEnumerable<DriverCarViewItem> Drivers { get; set; }
    }
}
