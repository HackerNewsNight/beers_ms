using System.Data.Entity.ModelConfiguration;
using BeerApp.Models; 

namespace BeerApp.Data.Configuration
{
    public class StyleConfiguration : EntityTypeConfiguration<Style>
    {
        public StyleConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100); 
        }
    }
}
