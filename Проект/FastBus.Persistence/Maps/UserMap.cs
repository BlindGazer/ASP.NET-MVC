using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities.Identity;

namespace FastBus.Persistence.Maps
{
    public class UserMap: EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");
            HasKey(u => u.Id);

            Property(u => u.RegistredDate).HasColumnType("date");
            Property(u => u.DateBorn).HasColumnType("date");
            Property(u => u.FirstName).IsRequired().HasMaxLength(25);
            Property(u => u.LastName).IsRequired().HasMaxLength(25);
            Property(u => u.Patronymic).HasMaxLength(25);
            Property(u => u.UserName).IsRequired().HasMaxLength(50);
        }
    }
}
