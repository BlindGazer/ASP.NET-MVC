using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FastBus.Domain.Objects;
using FastBus.Web.Helpers;

namespace FastBus.Web.Models.Route
{
    public enum RouteOrderByField
    {
        [Display(Name = "Место отправления")]
        Departure,
        [Display(Name = "Место назначения")]
        Destination
    }
    
    public class RouteSearchModel : BaseQuery
    {
        [DisplayName("Место отправления")]
        public string Departure { get; set; }
        [DisplayName("Место назначения")]
        public string Destination { get; set; }
        [DisplayName("Промежуточный пункт")]
        public string WayPoint { get; set; }
        public IEnumerable<SelectListItem> OrderByFields { get; set; }

        public RouteSearchModel()
        {
            OrderByFields = SelectListHelper.GetFields<RouteOrderByField>();
            OrderBy = new ColumnOrder(OrderByFields.First().Value);
        }
    }
}
