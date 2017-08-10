using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class PropertieLogMap : EntityTypeConfiguration<PropertieLog>
    {
        public PropertieLogMap()
        {
            HasKey(c => c.Id);
        }
    }
}
