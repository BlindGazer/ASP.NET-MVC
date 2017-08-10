using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {
            HasKey(t => t.Id);
        }
    }
}
