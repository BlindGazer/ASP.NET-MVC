using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FastBus.DAL.Concrete.Entities;

namespace FastBus.DAL.Concrete.Maps
{
    public class ReviewMap : EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            HasKey(rw => rw.Id);
            Property(rw => rw.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(rw => rw.Message).HasMaxLength(500).IsRequired();
        }
    }
}
