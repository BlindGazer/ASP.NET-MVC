using System;

namespace FastBus.Domain.Constracts
{
    public abstract class BaseSchedule<TKey>: BaseEntity<TKey>, IBaseSchedule<TKey> where TKey: struct
    {
        public DateTime DepartureDate { get; set; }
        public DateTime DestinationDate { get; set; }

        public int CarId { get; set; }
        public int DispatcherId { get; set; }
        public decimal? Cost { get; set; }
    }
}
