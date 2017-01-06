using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class DriverRequiringApprovalMap : EntityTypeConfiguration<DriverRequiringApproval>
    {
        public DriverRequiringApprovalMap()
        {
            ToTable("DriversRequiringApproval");

            HasKey(r => r.UserId);

            Property(u => u.DateBorn).HasColumnType("date");
            Property(u => u.Name).IsRequired().HasMaxLength(75);

            HasMany(u => u.Cars).WithMany(c => c.DriversRequiringApproval).Map(m => m.ToTable("DriversRequiringApprovalCars").MapLeftKey("DriverId").MapRightKey("CarId")); }
    }
}
