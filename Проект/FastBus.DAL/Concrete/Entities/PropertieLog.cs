using FastBus.DAL.Constracts;

namespace FastBus.DAL.Concrete.Entities
{
    public class PropertieLog : BaseEntity<long>
    {
        public virtual string PropertyName { get; set; }
        public virtual string OriginalValue { get; set; }
        public virtual string NewValue { get; set; }
        public virtual HistoryLog Log { get; set; }
        public virtual long LogId { get; set; }
    }
}
