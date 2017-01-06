using System;

namespace FastBus.DAL.Constracts
{
    public abstract class BaseRoute<TKey>: BaseEntity<TKey> where TKey: struct
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? DestinationDate { get; set; }
        public int CarId { get; set; }
        public int CreaterId { get; set; }
        public decimal? Cost { get; set; }

    }
}
