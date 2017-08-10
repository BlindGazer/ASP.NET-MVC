using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class DriverRequiringApprovalMap : EntityTypeConfiguration<DriverRequiringApproval>
    {
        public DriverRequiringApprovalMap()
        {
            ToTable("DriversRequiringApproval");

            HasKey(r => r.DriverId);

            Property(u => u.DateBorn).HasColumnType("date");
            Property(u => u.Name).IsRequired().HasMaxLength(75);

            HasMany(u => u.Cars).WithMany(c => c.DriversRequiringApproval).
                Map(m => m.ToTable("DriversRequiringApprovalCars").MapLeftKey("DriverId").MapRightKey("CarId")); }
    }
}
