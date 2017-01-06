using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class PropertieLogMap : EntityTypeConfiguration<PropertieLog>
    {
        public PropertieLogMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
