using FastBus.DAL.Enums;
using FastBus.DAL.Objects;

namespace FastBus.Services.Models.Car
{
    public class CarSearchQuery : BaseQuery
    {
        public short? YearFrom { get; set; }
        public short? YearTo { get; set; }
        public string Model { get; set; }
        public string GovermentNumber { get; set; }
        public string Color { get; set; }
        public string DriverName { get; set; }
        public StatusCar? Status { get; set; }

    }
}
