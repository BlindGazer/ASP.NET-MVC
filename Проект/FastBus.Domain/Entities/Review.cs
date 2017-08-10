using FastBus.Domain.Constracts;
using FastBus.Domain.Enums;

namespace FastBus.Domain.Entities
{
    public class Review : BaseEntity<int>
    {
        public string Message { get; set; }
        public int BuyerId { get; set; }
        public TypeReview Type { get; set; }


        public virtual Buyer Buyer { get; set; }
    }
}
