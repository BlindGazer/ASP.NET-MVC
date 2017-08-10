using System.Data.Entity.ModelConfiguration;
using FastBus.Domain.Entities;

namespace FastBus.Persistence.Maps
{
    public class ReviewMap : EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            HasKey(rw => rw.Id);
            Property(rw => rw.Message).HasMaxLength(500).IsRequired();
        }
    }
}
