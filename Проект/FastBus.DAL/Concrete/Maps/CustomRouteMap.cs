using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class CustomRouteMap : EntityTypeConfiguration<CustomRoute>
    {
        public CustomRouteMap()
        {
            HasRequired(cr => cr.Creater).WithMany(u => u.CreaterCustomRoutes).HasForeignKey(cr => cr.CreaterId).WillCascadeOnDelete(false);
            HasRequired(cr => cr.Car).WithMany(c => c.CustomRoutes).HasForeignKey(r => r.CarId).WillCascadeOnDelete(false);
            HasRequired(cr => cr.Customer).WithMany(u => u.Orders).HasForeignKey(cr => cr.CustomerId).WillCascadeOnDelete(false);
        }
    }
}
