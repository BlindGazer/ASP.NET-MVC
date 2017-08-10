using Microsoft.AspNet.Identity.EntityFramework;

namespace FastBus.Domain.Entities.Identity
{
    public class Role : IdentityRole<int, UserRole>
    {
        public string Description { get; set; }
    }
}
