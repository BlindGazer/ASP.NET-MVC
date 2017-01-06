namespace FastBus.DAL.Concrete.Entities
{
    public class RouteWayPoint
    {
        public int WayPointId { get; set; }
        public long RouteId { get; set; }
        public byte Sequence { get; set; }

        public Route Route { get; set; }
        public WayPoint WayPoint { get; set; }
    }
}
