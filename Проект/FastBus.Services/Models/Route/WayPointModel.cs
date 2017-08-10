using FastBus.Domain.Objects;

namespace FastBus.Services.Models.Route
{
    public class WayPointModel
    {
        public int RouteId { get; set; }
        public int WayPointId { get; set; }
        public byte Order { get; set; }
        public ListItem WayPoint { get; set;}
    }
}
