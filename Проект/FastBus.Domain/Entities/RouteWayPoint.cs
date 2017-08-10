namespace FastBus.Domain.Entities
{
    public class RouteWayPoint
    {
        public int WayPointId { get; set; }
        public int RouteId { get; set; }
        public byte Order { get; set; }

        public Route Route { get; set; }
        public WayPoint WayPoint { get; set; }
    }
}
