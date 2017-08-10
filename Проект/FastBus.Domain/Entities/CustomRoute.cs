using System;
using System.Collections.Generic;
using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class CustomRoute : BaseSchedule<int>
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public int BuyerId { get; set; }
        public DateTime ReturnDate { get; set; }
        public short? Distane { get; set; }
        public string Comment { get; set; }

        public virtual Dispatcher Dispatcher { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual Car Car { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
