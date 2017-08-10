using System;

namespace FastBus.Domain.Constracts
{
    public interface IBaseSchedule<TKey>: IBaseEntity<TKey> where TKey : struct
    {
        DateTime DepartureDate { get; set; }
        DateTime DestinationDate { get; set; }

        int CarId { get; set; }
        int DispatcherId { get; set; }
        decimal? Cost { get; set; }
    }
}
