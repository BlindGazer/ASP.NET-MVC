using System;
using System.Collections.Generic;
using FastBus.Domain.Objects;
using FastBus.Services.Models.Car;

namespace FastBus.Services.Models.Route
{
    public class ScheduleModel
    {
        public long Id { get; set; }
        public int RouteId { get; set; }
        public int Number { get; set; }
        public RouteModel Route { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime DestinationDate { get; set; }
        public byte Seats { get; set; }
        public int CarId { get; set; }
        public CarModel Car { get; set; }
        public int DispatcherId { get; set; }
        public string DispatcherName { get; set; }
        public decimal? Cost { get; set; }

        public List<ListItem> Drivers { get; set; }
        public IEnumerable<TicketModel> Tickets { get; set; }
    }
}
