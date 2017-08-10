using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            HasKey(r => r.Id);

            Property(r => r.Name).IsRequired().HasMaxLength(50);
            Property(r => r.Republic).IsRequired().HasMaxLength(25);
            Property(r => r.Decription).IsRequired().HasMaxLength(500);
            Property(r => r.Address).IsRequired().HasMaxLength(200);
            Property(r => r.Phones).IsRequired().HasMaxLength(200);
            Property(r => r.Emails).IsRequired().HasMaxLength(200);
            Property(r => r.PostCode).IsRequired().HasMaxLength(200);
        }
    }
}
