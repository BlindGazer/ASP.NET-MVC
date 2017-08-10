namespace FastBus.Web.Models.Route
{
    public class TicketSearchModel : ScheduleSearchModel
    {
        public int? BuyerId { get; set; }
        public bool? IsPayment { get; set; }
    }
}
