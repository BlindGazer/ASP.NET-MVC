using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class WayPointMap : EntityTypeConfiguration<WayPoint>
    {
        public WayPointMap()
        {
            HasKey(wp => wp.Id);
            Property(wp => wp.Name).IsRequired().HasMaxLength(30);

            HasMany(r => r.Routes).WithRequired(wp => wp.WayPoint).HasForeignKey(t => t.WayPointId).WillCascadeOnDelete(false);
        }
    }
}
