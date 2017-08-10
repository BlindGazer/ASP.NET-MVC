using System;

namespace FastBus.Web.Models.Route
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public long ScheduleId { get; set; }
        public bool IsReserve { get; set; }
        public bool IsPaid { get; set; }
        public int Number { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime DestinationDate { get; set; }
        public decimal? Cost { get; set; }

        public string Status => IsPaid ? "Куплен" : "Забронирован"; 
        public string CostFormated => Cost.HasValue ? $"{Cost} руб" : null;
        public TimeSpan InTransitTime => DestinationDate - DepartureDate;
        public string Time => $"{InTransitTime.Hours:00}:{InTransitTime.Minutes:00}";
    }
}
