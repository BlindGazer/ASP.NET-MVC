using System;
using System.ComponentModel;

namespace FastBus.Web.Models.Route
{
    public class ScheduleSearchModel : RouteSearchModel
    {
        [DisplayName("Дата отправления")]
        public DateTime DepartureDate { get; set; }
        [DisplayName("Дата назначения")]
        public DateTime DestinationDate { get; set; }

        public ScheduleSearchModel(DateTime date)
        {
            DepartureDate = date;
            DestinationDate = date.AddDays(7);
        }

        public ScheduleSearchModel() { }
    }
}
