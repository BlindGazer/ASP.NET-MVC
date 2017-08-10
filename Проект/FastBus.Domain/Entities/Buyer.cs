using System.Collections.Generic;
using FastBus.Domain.Entities.Identity;

namespace FastBus.Domain.Entities
{
    public class Buyer : User
    {
        public bool IsCanReserve { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<CustomRoute> Orders { get; set; }

        public Buyer()
        {
            IsCanReserve = true;
        }
    }
}
