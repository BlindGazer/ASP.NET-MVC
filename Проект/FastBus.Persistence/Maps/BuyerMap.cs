using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class BuyerMap : EntityTypeConfiguration<Buyer>
    {
        public BuyerMap()
        {
            HasMany(u => u.Reviews).WithRequired(r => r.Buyer).HasForeignKey(r => r.BuyerId).WillCascadeOnDelete(false);
            HasMany(u => u.Tickets).WithRequired(t => t.Buyer).HasForeignKey(t => t.BuyerId).WillCascadeOnDelete(false);
            HasMany(u => u.Orders).WithRequired(cr => cr.Buyer).HasForeignKey(cr => cr.BuyerId).WillCascadeOnDelete(false);
        }
    }
}
