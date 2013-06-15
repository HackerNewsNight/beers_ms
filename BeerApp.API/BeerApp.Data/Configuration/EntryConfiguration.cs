using System.Data.Entity.ModelConfiguration;
using BeerApp.Models;

namespace BeerApp.Data.Configuration
{
    public class EntryConfiguration : EntityTypeConfiguration<Entry>
    {
        public EntryConfiguration()
        {
            Property(p => p.Comment).IsOptional().HasMaxLength(300);
            Property(p => p.Date).IsRequired().HasColumnType("datetime");
            Property(p => p.Score).IsRequired(); 

        }
    }
}
