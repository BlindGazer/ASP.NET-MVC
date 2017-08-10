using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class HistoryLogMap : EntityTypeConfiguration<HistoryLog>
    {
        public HistoryLogMap()
        {
            HasKey(c => c.Id);

            HasMany(c => c.Properties).WithRequired(r => r.Log).HasForeignKey(c => c.LogId).WillCascadeOnDelete(false);
        }
    }
}
