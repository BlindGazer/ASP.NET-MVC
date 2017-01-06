using System;
using System.Collections.Generic;
using FastBus.DAL.Constracts;
using FastBus.DAL.Enums;

namespace FastBus.DAL.Concrete.Entities
{
    public class HistoryLog : BaseEntity<long>
    {
        public DateTime LogDate { get; set; }
        public int? UserId { get; set; }

        public HistoryStatus Status { get; set; }
        public ICollection<PropertieLog> Properties { get; set; }

    }
}
