using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class ScheduleItemMap : EntityTypeConfiguration<ScheduleItem>
    {
        public ScheduleItemMap()
        {
            HasKey(r => r.Id);
            
            HasMany(r => r.Tickets).WithRequired(t => t.ScheduleItem).HasForeignKey(t => t.ScheduleId).WillCascadeOnDelete(false);

            HasMany(u => u.Drivers).WithMany(cr => cr.Schedule).
                Map(m => m.ToTable("DriversSchedule").MapLeftKey("DriverId").MapRightKey("ScheduleItemId"));

        }
    }
}
