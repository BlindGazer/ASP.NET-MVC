using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FastBus.DAL.Concrete.Entities.Identity
{
    public class User: IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public string Name { get; set; }
        public DateTime RegistredDate { get; set; }
        public DateTime? DateBorn { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<CustomRoute> Orders { get; set; }
        public virtual ICollection<CustomRoute> DriverCustomRoutes { get; set; }
        public virtual ICollection<CustomRoute> CreaterCustomRoutes { get; set; }
        public virtual ICollection<Route> DriverRoutes { get; set; }
        public virtual ICollection<Route> CreaterRoutes { get; set; }
    }
}
