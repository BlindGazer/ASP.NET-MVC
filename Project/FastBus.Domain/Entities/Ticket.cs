using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class Ticket: BaseEntity<int>
    {
        public int BuyerId { get; set; }
        public long ScheduleId { get; set; }
        public bool IsReserve { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDisabled { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ScheduleItem ScheduleItem { get; set; }
    }
}
