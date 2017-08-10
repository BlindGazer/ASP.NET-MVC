using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FastBus.Domain.Entities.Identity
{
    public class User: IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime RegistredDate { get; set; }
        public DateTime? DateBorn { get; set; }
    }
}
