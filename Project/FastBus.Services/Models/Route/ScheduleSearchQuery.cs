using System;

namespace FastBus.Services.Models.Route
{
    public class ScheduleSearchQuery : RouteSearchQuery
    {
        public DateTime? DepartureDate { get; set; }
        public DateTime? DestinationDate { get; set; }
    }
}
