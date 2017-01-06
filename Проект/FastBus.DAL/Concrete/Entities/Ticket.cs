using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Constracts;

namespace FastBus.DAL.Concrete.Entities
{
    public class Ticket: BaseEntity<int>
    {
        public int UserId { get; set; }
        public long RouteId { get; set; }
        public bool IsReserve { get; set; }

        public virtual User User { get; set; }
        public virtual Route Route { get; set; }
    }
}
