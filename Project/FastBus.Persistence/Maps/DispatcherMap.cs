using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class DispatcherMap : EntityTypeConfiguration<Dispatcher>
    {
        public DispatcherMap()
        {
            HasMany(u => u.Schedule).WithRequired(cr => cr.Dispatcher).HasForeignKey(cr => cr.DispatcherId).WillCascadeOnDelete(false);
            HasMany(u => u.CustomRoutes).WithRequired(cr => cr.Dispatcher).HasForeignKey(cr => cr.DispatcherId).WillCascadeOnDelete(false);
        }
    }
}
