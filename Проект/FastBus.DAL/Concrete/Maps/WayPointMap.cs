using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class WayPointMap : EntityTypeConfiguration<WayPoint>
    {
        public WayPointMap()
        {
            HasKey(wp => wp.Id);
            Property(wp => wp.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(wp => wp.Name).IsRequired().HasMaxLength(30);

            HasMany(r => r.Routes).WithRequired(wp => wp.WayPoint).HasForeignKey(t => t.WayPointId).WillCascadeOnDelete(false);
        }
    }
}
