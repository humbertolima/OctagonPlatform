using System.Data.Entity.ModelConfiguration;

namespace OctagonPlatform.Models.EntityConfigurations
{
    public class CountryConfiguration:EntityTypeConfiguration<Country>
    {

        public CountryConfiguration()
        {
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(20);

            HasMany(c => c.Stateses)
                .WithRequired(s => s.Country)
                .WillCascadeOnDelete(false);

        }
    }
}