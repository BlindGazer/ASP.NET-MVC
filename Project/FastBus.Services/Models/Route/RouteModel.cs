using System.Collections.Generic;

namespace FastBus.Services.Models.Route
{
    public class RouteModel
    {
        public int Id { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public IEnumerable<WayPointModel> WayPoints { get; set; }
    }
}
