using System.Data.Entity.ModelConfiguration;

namespace OctagonPlatform.Models.EntityConfigurations
{
    public class PartnerContactConfiguration: EntityTypeConfiguration<Partner>
    {
        public PartnerContactConfiguration()
        {
            Property(p => p.CountryId)
              .IsRequired();
              
            
        }
    }
}