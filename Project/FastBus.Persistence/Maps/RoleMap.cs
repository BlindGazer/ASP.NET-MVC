using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities.Identity;

namespace FastBus.Persistence.Maps
{
    public class RoleMap: EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Roles");
            HasKey(u => u.Id);

            Property(u => u.Name).IsRequired().HasMaxLength(50);
            Property(u => u.Description).IsRequired().HasMaxLength(50);
        }
    }
}
