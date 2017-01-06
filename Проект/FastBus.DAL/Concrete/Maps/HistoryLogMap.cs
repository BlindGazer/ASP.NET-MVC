using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class HistoryLogMap : EntityTypeConfiguration<HistoryLog>
    {
        public HistoryLogMap()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(c => c.Properties).WithRequired(r => r.Log).HasForeignKey(c => c.LogId).WillCascadeOnDelete(false);
        }
    }
}
