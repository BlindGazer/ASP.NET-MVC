using Microsoft.AspNet.Identity.EntityFramework;

namespace FastBus.DAL.Concrete.Entities.Identity
{
    public class Role : IdentityRole<int, UserRole>
    {
        public string Description { get; set; }
    }
}
