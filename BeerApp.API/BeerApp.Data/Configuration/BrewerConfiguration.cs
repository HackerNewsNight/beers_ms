using System.Data.Entity.ModelConfiguration;
using BeerApp.Models;

namespace BeerApp.Data.Configuration
{
    public class BrewerConfiguration : EntityTypeConfiguration<Brewer>
    {
        public BrewerConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100);
        }
    }
}
