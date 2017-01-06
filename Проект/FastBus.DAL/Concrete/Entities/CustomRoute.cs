using System;
using System.Collections.Generic;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Constracts;

namespace FastBus.DAL.Concrete.Entities
{
    public class CustomRoute : BaseRoute<long>
    {
        public DateTime ReturnDate { get; set; }
        public short? Distane { get; set; }
        public string Other { get; set; }
        public int CustomerId { get; set; }


        public virtual ICollection<User> Drivers { get; set; }

        public virtual User Creater { get; set; }
        public virtual Car Car { get; set; }
        public virtual User Customer { get; set; }
    }
}
