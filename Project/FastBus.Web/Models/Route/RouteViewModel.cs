using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FastBus.Web.Models.Route
{
    public class RouteViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Место отправления")]
        public string Departure { get; set; }
        [Required]
        [Display(Name = "Место назначения")]
        public string Destination { get; set; }
        public IEnumerable<WayPointViewModel> WayPoints { get; set; }
        public string WayPointsString => WayPoints == null ? "" :
            string.Join("-", WayPoints.OrderBy(x => x.Order).Select(x => x.WayPoint));
    }
}
