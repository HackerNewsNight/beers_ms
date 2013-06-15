using System.Data.Entity.ModelConfiguration;
using BeerApp.Models;

namespace BeerApp.Data.Configuration
{
    public class BeerConfiguration : EntityTypeConfiguration<Beer>
    {
        public BeerConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100);
        }
    }
}
