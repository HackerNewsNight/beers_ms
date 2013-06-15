using System.Data.Entity.ModelConfiguration;
using BeerApp.Models;

namespace BeerApp.Data.Configuration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100);
        }
    }
}
