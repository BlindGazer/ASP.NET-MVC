using System;
using System.Collections.Generic;
using FastBus.Domain.Enums;
using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class HistoryLog : BaseEntity<long>
    {
        public DateTime Create { get; set; }
        public int? UserId { get; set; }
        public int? BuyerId { get; set; }
        public int? DispatcherId { get; set; }
        public int? DriverId { get; set; }
        public int? RouteId { get; set; }
        public long? ScheduleItemId { get; set; }
        public int? CarId { get; set; }

        public HistoryStatus Status { get; set; }
        public ICollection<PropertieLog> Properties { get; set; }

    }
}
