using System.Data.Entity.ModelConfiguration;
using BeerApp.Models;

namespace BeerApp.Data.Configuration
{
    public class ContactDetailsConfiguration : EntityTypeConfiguration<ContactDetails>
    {
        public ContactDetailsConfiguration()
        {
            Property(p => p.Address1).IsRequired().HasMaxLength(255);
            Property(p => p.Address2).IsOptional();
            Property(p => p.Country).IsRequired().HasMaxLength(100);
            Property(p => p.City).IsRequired().HasMaxLength(100);
            Property(p => p.State).IsRequired().HasMaxLength(40);
            Property(p => p.Website).IsOptional().HasMaxLength(255);
            Property(p => p.Phone).IsOptional().HasMaxLength(15);
        }
    }
}
