namespace FastBus.Services.Models.Route
{
    public class TicketModel
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public long ScheduleId { get; set; }
        public bool IsReserve { get; set; }
        public bool IsPaid { get; set; }
    }
    public class TicketModelWithSchedule : TicketModel
    {
        public ScheduleModel Schedule { get; set; }
    }
}
