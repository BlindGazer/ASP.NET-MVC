using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Constracts;
using FastBus.DAL.Enums;

namespace FastBus.DAL.Concrete.Entities
{
    public class Review : BaseEntity<int>
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public TypeReview Type { get; set; }


        public virtual User User { get; set; }
    }
}
