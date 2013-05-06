using System.Data.Entity;
using System.Linq;
using BeerApp.Domain;

namespace BeerApp.Web.Infrastructure
{
    public class BeerDb : DbContext, IBeerDataSource
    {
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Entry> Entries { get; set; }

        IQueryable<Beer> IBeerDataSource.Beers
        {
            get { return Beers; }
        }

        IQueryable<Entry> IBeerDataSource.Entries
        {
            get { return Entries; }
        }
    }
}