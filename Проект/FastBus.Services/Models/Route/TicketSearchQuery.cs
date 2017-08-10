using System;
using FastBus.Domain.Objects;

namespace FastBus.Services.Models.Route
{
    public class TicketSearchQuery : ScheduleSearchQuery
    {
        public int? BuyerId { get; set; }
        public bool? IsPayment { get; set; }

        public TicketSearchQuery() { }

        public TicketSearchQuery(int buyerId)
        {
            var now = DateTime.Now.Date;
            DepartureDate = now;
            DestinationDate = now.AddDays(7);
            BuyerId = buyerId;
            Paging = new Paging
            {
                Length = 100,
                Page = 1
            };
        }
    }
}
