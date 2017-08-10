using FastBus.Domain.Constracts;

namespace FastBus.Domain.Entities
{
    public class Company : BaseEntity<byte>
    {
        public string Name { get; set; }
        public string Republic { get; set; }
        public string Decription { get; set; }
        public string Address { get; set; }
        public string Phones { get; set; }
        public string Emails { get; set; }
        public string PostCode { get; set; }
    }
}
