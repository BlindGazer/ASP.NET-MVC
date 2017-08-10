using FastBus.Domain.Objects;

namespace FastBus.Services.Models.Route
{
    public class RouteSearchQuery : BaseQuery
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string WayPoint { get; set; }
    }
}
