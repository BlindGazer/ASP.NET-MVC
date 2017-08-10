using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class PropertieLog : BaseEntity<long>
    {
        public string PropertyName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public long LogId { get; set; }

        public virtual HistoryLog Log { get; set; }
    }
}
