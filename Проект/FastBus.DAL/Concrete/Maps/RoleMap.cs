using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities.Identity;

namespace FastBus.DAL.Concrete.Maps
{
    public class RoleMap: EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Roles");
            Property(u => u.Name).IsRequired().HasMaxLength(50);
            Property(u => u.Description).IsRequired().HasMaxLength(50);
        }
    }
}
