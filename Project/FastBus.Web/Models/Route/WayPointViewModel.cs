using System.ComponentModel.DataAnnotations;
using FastBus.Domain.Objects;

namespace FastBus.Web.Models.Route
{
    public class WayPointViewModel
    {
        public int RouteId { get; set; }
        public int WayPointId { get; set; }
        [Required, Range(1, byte.MaxValue)]
        public byte Order { get; set; }
        public ListItem WayPoint { get; set;}
}
}
